using Learn_mvc.Attributes;
using Learn_mvc.Data.IdentityDbContextFolder;
using Learn_mvc.Data.IdentityModels;
using Learn_mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Controllers
{
    public class IdentityExampleController : Controller
    {
        private MySignInManager _signInManager;
        private MyUserManager _userManager;

        public IdentityExampleController()
        {
        }

        public IdentityExampleController(MyUserManager userManager, MySignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public MySignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<MySignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public MyUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<MyUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            IdentityLoginModel model = new IdentityLoginModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(IdentityLoginModel model)
        {

            try
            {
                var signedUser = UserManager.FindByEmail(model.UserEmail);
                var result = await SignInManager.PasswordSignInAsync(signedUser.UserName, model.UserPassword, false, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToAction("Home");
                    case SignInStatus.LockedOut:
                    //return View("Lockout");
                    case SignInStatus.RequiresVerification:
                    //return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("My Error", "Invalid login attempt.");
                        return View(model);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult Register()
        {
            IdentityRegisterModel model = new IdentityRegisterModel();
            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(IdentityRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new MyIdentityUser { UserName = model.UserEmail, Email = model.UserEmail };
                var result = await UserManager.CreateAsync(user, model.UserPassword); //adding user in "AspNetUsers" table
                if (result.Succeeded)
                {
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
                    if (!await roleManager.RoleExistsAsync("User"))
                    {
                        var role = new IdentityRole();
                        role.Name = "User";
                        await roleManager.CreateAsync(role);
                    }
                    await UserManager.AddToRoleAsync(user.Id, "User"); //adding user in "AspNetUserRoles" table

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Home", "IdentityExample");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [CustomAuthorize]
        public ActionResult Home()
        {
            ViewBag.UserName = User.Identity.Name;
            ViewBag.UserId = User.Identity.GetUserId();
            return View();
        }

        [CustomAuthorize]
        public ActionResult UserPage()
        {
            
            return View();
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult AdminPage()
        {
            return View();
        }

        [CustomAuthorize]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "IdentityExample");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
using Learn_mvc.Helper;
using Learn_mvc.Models;
using Learn_mvc.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Learn_mvc.Controllers
{
    public class FormBaseAuthController : Controller
    {
        // GET: FormBaseAuth
        public ActionResult Index()
        {
            BigViewModel bigViewModel = new BigViewModel { 
                UserViewModel = new User()
            };
            return View(bigViewModel);
        }

        [HttpPost]
        public ActionResult Login(BigViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            if (ModelState.IsValid)
            {
                var user = GetUserData(userModel.UserViewModel.UserEmail);
                if (user!=null)
                {
                    string userData = JsonConvert.SerializeObject(user);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                    (
                        1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                    );

                    string enTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie("UserCookie", enTicket);
                    Response.Cookies.Add(faCookie);

                    if (user.UserRole == "User1")
                    {
                        return RedirectToAction("User1", "FormBaseAuth", null);
                    }
                    else if (user.UserRole == "User2")
                    {
                        return RedirectToAction("User2", "FormBaseAuth", null);
                    }
                }
                else
                {
                    return View("Index");
                }
            }
            return View("Index");
        }

        public User GetUserData(string email)
        {
            if (email == "user1@gmail.com")
            {
                return new User { UserId = 1, UserEmail = "user1@gmail.com", UserName = "User 1", UserPassword = "111111", UserRole = "User1" };
            }
            else if(email == "user2@gmail.com")
            {
                return new User { UserId = 2, UserEmail = "user2@gmail.com", UserName = "User 2", UserPassword = "111111", UserRole = "User2" };
            }
            else
            {
                return null;
            }
        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("UserCookie", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "FormBaseAuth", null);
        }

        [CustomAuthorize(Roles = "User1")]
        public ActionResult User1()
        {
            var customPrincipal = HttpContext.User as CustomPrincipal;
            ViewBag.UserEmail = customPrincipal.UserEmail;
            return View();
        }

        [CustomAuthorize(Roles = "User2")]
        public ActionResult User2()
        {
            return View();
        }
    }
}
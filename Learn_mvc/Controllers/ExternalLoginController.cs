using Microsoft.Web.WebPages.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Controllers
{
    public class ExternalLoginController : Controller
    {
        // GET: ExternalLogin
        public ActionResult Index()
        {
            var a = Request.Url.GetLeftPart(UriPartial.Authority);
            return View();
        }

        public ActionResult GmailFacebookLogin(string providerName)
        {
            OAuthWebSecurity.RequestAuthentication(providerName, Url.Action("ExternalLoginCallback"));
            return RedirectToAction("Index", "ExternalLogin");
        }

        public ActionResult ExternalLoginCallback()
        {
            var result = OAuthWebSecurity.VerifyAuthentication();

            if (result.IsSuccessful == false)
            {
                TempData["Msg"] = "Login Failed";
                return RedirectToAction("Index", "ExternalLogin");
            }
            else
            {
                TempData["Msg"] = "Login Successfully";
                return RedirectToAction("Index", "ExternalLogin");
            }
        }
    }
}
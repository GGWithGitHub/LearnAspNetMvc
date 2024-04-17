using Learn_mvc.Models;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Learn_mvc.ExtensionMethods;

namespace Learn_mvc.Controllers
{
    public class SecuretyController : Controller
    {
        public static List<UserComment> userComments = new List<UserComment>();
        public ActionResult Index()
        {
            ViewBag.usrcomments = userComments;
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult Xss()
        {
            UserComment userComment = new UserComment();
            userComment.username = Request.Form["username"].ToString();
            userComment.useremail = Request.Form["useremail"].ToString();
            //userComment.usercomments = Sanitizer.GetSafeHtmlFragment(Request.Form["usercomments"].ToString()); // This is also a good approach
            //userComment.usercomments = Request.Form["usercomments"].ToString().IsSafeHtml(); // This is also a good approach
            userComment.usercomments = Request.Form["usercomments"].ToString();

            object obj = SafeHtml(userComment);

            userComments.Add(obj as UserComment);

            TempData["msg"] = "Form Submitted !!";

            return RedirectToAction("Index");
        }

        public object SafeHtml(object parameter)
        {
            foreach (var prop in parameter.GetType().GetProperties())
            {
                string PropertyType = prop.PropertyType.Name;

                if (PropertyType.ToString().Trim() == "String")
                {
                    if (prop.GetValue(parameter, null) != null)
                    {
                        string PropertyValue = prop.GetValue(parameter).ToString();
                        string NewPropertyValue = Sanitizer.GetSafeHtmlFragment(PropertyValue);
                        prop.SetValue(parameter, NewPropertyValue);
                    }

                }
            }
            return parameter;
        }
    }
}
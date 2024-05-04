using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated) // User is not authenticated
            {
                httpContext.Response.Redirect("~/IdentityExample/Login");
            }

            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized) // User is not authorized
            {
                httpContext.Response.Redirect("~/IdentityExample/Home");
            }

            if (Roles.Length==0 || Roles.Split(',').Any(role => httpContext.User.IsInRole(role)))
            {
                return true;
            }
            return false;
        }
    }
}
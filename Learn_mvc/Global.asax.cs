using Learn_mvc.App_Start;
using Learn_mvc.Helper;
using Learn_mvc.Models;
using Microsoft.Web.WebPages.OAuth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Learn_mvc
{
    public static class OAuthConfig
    {
        public static void RegisterProviders()
        {
            OAuthWebSecurity.RegisterGoogleClient();

            // find AppId and AppSecret in "Web.config -> appSettings"
            OAuthWebSecurity.RegisterFacebookClient(appId: ConfigurationManager.AppSettings["AppId"], appSecret: ConfigurationManager.AppSettings["AppSecret"]);
        }
    }
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            OAuthConfig.RegisterProviders();

            GlobalFilters.Filters.Add(new ValidateInputAttribute(false));
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["UserCookie"];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var serializeModel = JsonConvert.DeserializeObject<User>(authTicket.UserData);
                CustomPrincipal principal = new CustomPrincipal(authTicket.Name);
                principal.UserId = serializeModel.UserId;
                principal.UserName = serializeModel.UserName;
                principal.UserEmail = serializeModel.UserEmail;
                principal.UserPassword = serializeModel.UserPassword;
                principal.Roles = serializeModel.UserRole;
                HttpContext.Current.User = principal;
            }

        }
    }
}

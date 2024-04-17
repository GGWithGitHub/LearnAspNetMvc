using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn_mvc.ExtensionMethods
{
    public static class SafeHtmlExtMethod
    {
        public static string IsSafeHtml(this string str)
        {
            return Sanitizer.GetSafeHtmlFragment(str);
        }
    }
}
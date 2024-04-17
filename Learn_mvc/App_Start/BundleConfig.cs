using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Learn_mvc.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new StyleBundle("~/bundles/css").Include(
                                                    "~/Content/Site.css",
                                                    "~/Content/css/bootstrap.min.css"
                                                 )
            );

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                                                            "~/Content/js/jquery.min.js",
                                                            "~/Content/js/bootbox.min.js",
                                                            "~/Content/js/bootstrap.min.js"
                                                        )
            );

            bundles.Add(new ScriptBundle("~/bundles/jsval").Include(
                                                            "~/Content/js/jquery.unobtrusive-ajax.min.js",
                                                            "~/Content/js/jquery.validate.min.js",
                                                            "~/Content/js/jquery.validate.unobtrusive.min.js"
                                                        )
            );

            bundles.Add(new ScriptBundle("~/bundles/commonjs").Include(
                                                            "~/Content/Common_JS.js"
                                                        )
            );
        }
    }
}
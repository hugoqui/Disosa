using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace DisosaWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.min.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/app").Include(                
                "~/Scripts/app/site.js"));


            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
                "~/Scripts/materialize.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/materialize.css",
                 "~/Content/Site.css"));
        }
    }
}

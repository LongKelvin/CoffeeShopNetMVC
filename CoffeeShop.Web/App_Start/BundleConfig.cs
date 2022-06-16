using CoffeeShop.Common;

using System.Web.Optimization;

namespace CoffeeShop.Web
{
    public class BundleConfig
    {
        //For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery")
                .Include("~/Assets/Admin/lib/jquery/jquery-3.4.1.min.js"));

            bundles.Add(new ScriptBundle("~/js/jquery-ui")
               .Include("~/Assets/Admin/lib/jquery-ui-1.13.1.custom/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/js/plugins").Include(
                 "~/Assets/Admin/lib/jquery-ui-1.13.1.custom/jquery-ui.min.js",
                 "~/Assets/Client/js/popper.min.js",
                 //"~/Assets/admin/libs/mustache/mustache.js",
                 //"~/Assets/admin/libs/numeral/numeral.js",
                 //"~/Assets/Admin/lib/jquery/jquery.validate*",
                 //"~/Assets/Admin/lib/bootstrap/js/bootstrap.js",
                 "~/Assets/Client/js/custom.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                       "~/Assets/Admin/lib/jquery/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Assets/Admin/lib/bootstrap/js/bootstrap.js"));

            bundles.Add(new StyleBundle("~/css/base")
           .Include("~/Assets/Client/css/bootstrap.css", new CssRewriteUrlTransform())
           //.Include("~/Assets/Admin/lib/bootstrap/css/bootstrap.min.css.map", new CssRewriteUrlTransform())
           .Include("~/Assets/Client/css/font-awesome.min.css", new CssRewriteUrlTransform())
           .Include("~/Assets/Client/css/responsive.css", new CssRewriteUrlTransform())
           .Include("~/Assets/Client/custom-css/custom-style.css", new CssRewriteUrlTransform())
           .Include("~/Assets/Admin/lib/jquery-ui-1.13.1.custom/jquery-ui.theme.css", new CssRewriteUrlTransform())
           .Include("~/Assets/Client/css/style.css", new CssRewriteUrlTransform())

            );
            BundleTable.EnableOptimizations = bool.Parse(ConfigHelper.GetByKey("EnableBundles"));
        }
    }
}
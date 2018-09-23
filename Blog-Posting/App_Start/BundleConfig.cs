using System.Web;
using System.Web.Optimization;

namespace Blog_Posting
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/skdslider").Include(
                        "~/Scripts/skdslider.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                       "~/Scripts/main.js"));
            bundles.Add(new ScriptBundle("~/bundles/movetop").Include(
                       "~/Scripts/move-top.js"));
            bundles.Add(new ScriptBundle("~/bundles/easing").Include(
                       "~/Scripts/easing.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));
            bundles.Add(new StyleBundle("~/Content/fontawesome").Include("~/Content/font-awesome.min.css"));
            bundles.Add(new StyleBundle("~/Content/style").Include("~/Content/style.css"));
            bundles.Add(new StyleBundle("~/Content/flexslider").Include("~/Content/flexslider.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include( "~/Content/skdslider.css", "~/Content/flexslider.css", "~/Content/style.css",
                      "~/Content/site.css"));
        }
    }
}

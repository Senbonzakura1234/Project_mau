using System.Web;
using System.Web.Optimization;

namespace ASP.NET_Project
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                      "~/Scripts/popper.min.js", "~/Scripts/popper-utils.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));


            // bootstrap select
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-select-js").Include(
                        "~/Scripts/bootstrap-select.min.js"));
            //bundles.Add(new StyleBundle("~/Content/bootstrap-select-css").Include(
            //            "~/Content/bootstrap-select.css"));

            bundles.Add(new ScriptBundle("~/bundles/mainJs").Include(
                        "~/Scripts/main.js"));
            //bundles.Add(new StyleBundle("~/Content/mainCss").Include(
            //            "~/Content/site.css"));
            //bundles.Add(new StyleBundle("~/Content/cssTest").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));


            //new layout




            bundles.Add(new ScriptBundle("~/bundles/root-script").Include(
                "~/Content/vendors/js/vendor.bundle.base.js",
                "~/Content/vendors/chart.js/Chart.min.js",
                "~/Scripts/js/off-canvas.js", "~/Scripts/js/hoverable-collapse.js",
                "~/Scripts/js/misc.js", "~/Scripts/js/dashboard.js", "~/Scripts/js/todolist.js"));

            bundles.Add(new ScriptBundle("~/bundles/root-script-home").Include(
                "~/Content/vendors/owl-carousel/js/owl.carousel.min.js", "~/Content/vendors/aos/js/aos.js", "~/Scripts/js/landingpage.js"));


            BundleTable.EnableOptimizations = true;
        }
    }
}

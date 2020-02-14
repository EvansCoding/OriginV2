using System.Web.Optimization;

namespace OriginV2.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/script/Admin").Include(
            "~/Content/Admin/assets/js/vendor.min.js",
            "~/Scripts/sweetalert.min.js",
             "~/Content/Admin/assets/js/pages/sweet-alerts.init.js",
             "~/Content/Admin/assets/libs/toastr/toastr.min.js",
             "~/Content/Admin/assets/js/app.min.js",
             "~/Content/jquery-3.4.1.min.js"));

            bundles.Add(new StyleBundle("~/css/Admin").Include(
                    "~/Content/Admin/assets/libs/toastr/toastr.min.css",
                      "~/Content/Admin/assets/css/bootstrap.min.css",
                      "~/Content/Admin/assets/css/app.min.css",
                      "~/Content/Admin/assets/css/icons.min.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}

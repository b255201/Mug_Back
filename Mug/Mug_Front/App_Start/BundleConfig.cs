using System.Web;
using System.Web.Optimization;

namespace Mug_Front
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/style/css").Include(
                   //"~/Content/css/open-iconic-bootstrap.min.css",
                   "~/Content/css/animate.css",
                   "~/Content/css/owl.carousel.min.css",
                   "~/Content/css/owl.theme.default.min.css",
                   "~/Content/css/magnific-popup.css",
                   "~/Content/css/aos.css",
                   "~/Content/css/ionicons.min.css",
                   "~/Content/css/bootstrap-datepicker.css",
                   "~/Content/css/jquery.timepicker.css",
                   //"~/Content/css/flaticon.css",
                   //"~/Content/css/icomoon.css",
                   "~/Content/css/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                      "~/Scripts/jquery-3.3.1.min.js",
                      "~/Scripts/js/jquery-migrate-3.0.1.min.js",
                      "~/Scripts/js/popper.min.js",
                      "~/Scripts/js/bootstrap.min.js",
                      "~/Scripts/js/jquery.easing.1.3.js",
                      "~/Scripts/js/jquery.waypoints.min.js",
                      "~/Scripts/js/jquery.stellar.min.js",
                      "~/Scripts/js/owl.carousel.min.js",
                      "~/Scripts/js/jquery.magnific-popup.min.js",
                      "~/Scripts/js/aos.js",
                      "~/Scripts/js/jquery.animateNumber.min.js",
                      "~/Scripts/js/bootstrap-datepicker.js",
                      "~/Scripts/js/jquery.timepicker.min.js",
                      "~/Scripts/js/scrollax.min.js",
                      "~/Scripts/js/main.js"));




        }
    }
}

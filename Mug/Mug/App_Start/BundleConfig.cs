using System.Web;
using System.Web.Optimization;

namespace Mug
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

   
            // jquery datatable
            bundles.Add(new StyleBundle("~/Content/css/datatables").Include(
                      "~/Content/DataTables/css/jquery.dataTables.css",
                      "~/Content/DataTables/css/dataTables.tableTools.css",
                      "~/Content/DataTables/css/dataTables.responsive.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                      "~/Scripts/DataTables/jquery.dataTables.js",
                      "~/Scripts/DataTables/dataTables.tableTools.js",
                      "~/Scripts/DataTables/dataTables.responsive.js"));

            // metroc
            bundles.Add(new StyleBundle("~/assets/global/css").Include(
                      //"~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                      //"~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                      "~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                      "~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                      "~/Content/assets/global/css/components.min.css",
                      "~/Content/assets/global/css/plugins.min.css",
                      "~/Content/assets/pages/css/login-2.min.css",
                      "~/Content/assets/layouts/layout/css/layout.min.css",
                      "~/Content/assets/layouts/layout/css/themes/darkblue.min.css",
                      "~/Content/assets/layouts/layout/css/custom.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/assets").Include(
                    "~/Content/assets/global/plugins/jquery.min.js",
                    "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                    "~/Content/assets/global/plugins/js.cookie.min.js",
                    "~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                    "~/Content/assets/global/plugins/jquery.blockui.min.js",
                    "~/Content/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                    "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                    "~/Content/assets/global/scripts/app.min.js",
                    "~/Content/assets/layouts/layout/scripts/layout.min.js",
                    "~/Content/assets/layouts/layout/scripts/demo.min.js",
                    "~/Content/assets/layouts/global/scripts/quick-sidebar.min.js",
                    "~/Content/assets/layouts/global/scripts/quick-nav.min.js"
                    ));

        }
    }
}

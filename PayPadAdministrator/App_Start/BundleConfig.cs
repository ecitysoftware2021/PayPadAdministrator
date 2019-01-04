using System.Web;
using System.Web.Optimization;

namespace PayPadAdministrator
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                   "~/Scripts/bootstrap.min.js",
                  "~/Scripts/detect.js",
                  "~/Scripts/fastclick.js",
                  "~/Scripts/jquery.slimscroll.js",
                  "~/Scripts/jquery.blockUI.js",
                  "~/Scripts/waves.js",
                  "~/Scripts/wow.min.js",
                  "~/Scripts/jquery.nicescroll.js",
                  "~/Scripts/jquery.scrollTo.min.js",
                  "~/Scripts/jquery.core.js",
                  "~/Scripts/jquery.app.js",
                  "~/Scripts/plugins/bootstrap-table/dist/bootstrap-table.min.js",
                  "~/Scripts/pages/jquery.bs-table.js",
                  "~/Scripts/plugins/custombox/dist/custombox.min.js",
                  "~/Scripts/pages/legacy.min.js",
                  "~/Scripts/plugins/sweetalert/dist/sweetalert.min.js",
                  "~/Scripts/PayPad.js"
                  ));


            bundles.Add(new ScriptBundle("~/bundles/PopUp").Include(
                "~/Scripts/plugins/isotope/dist/isotope.pkgd.min.js",
                "~/Scripts/plugins/magnific-popup/dist/jquery.magnific-popup.min.js",
                "~/Scripts/PayPad.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Nestable").Include(
                "~/Scripts/plugins/nestable/jquery.nestable.js",
                "~/Scripts/pages/nestable.js"
                ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Scripts/plugins/bootstrap-table/dist/bootstrap-table.min.css",
                     "~/Content/Css/bootstrap.css",
                     "~/Content/Css/core.css",
                     "~/Content/Css/components.css",
                     "~/Content/Css/icons.css",
                     "~/Content/Css/pages.css",
                     "~/Content/Css/responsive.css",
                     "~/Scripts/plugins/sweetalert/dist/sweetalert.css"
                      ));


            bundles.Add(new StyleBundle("~/Content/Login").Include(
                     "~/Content/Css/bootstrap.css",
                     "~/Content/Css/core.css",
                     "~/Content/Css/components.css",
                     "~/Content/Css/icons.css",
                     "~/Content/Css/pages.css",
                     "~/Content/Css/responsive.css"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/Login").Include(
                  "~/Scripts/bootstrap.min.js",
                  "~/Scripts/detect.js",
                  "~/Scripts/fastclick.js",
                  "~/Scripts/jquery.slimscroll.js",
                  "~/Scripts/jquery.blockUI.js",
                  "~/Scripts/waves.js",
                  "~/Scripts/wow.min.js",
                  "~/Scripts/jquery.nicescroll.js",
                  "~/Scripts/jquery.scrollTo.min.js",
                  "~/Scripts/jquery.core.js",
                  "~/Scripts/jquery.app.js"
                  ));
        }
    }
}

using System.Web;
using System.Web.Optimization;

namespace PaypadConsoleLog
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/js/bootstrap.min.js",
                      "~/Scripts/js/wow.min.js",
                      "~/Scripts/js/jquery-price-slider.js",
                      "~/Scripts/js/jquery.meanmenu.js",
                      "~/Scripts/js/owl.carousel.min.js",
                      "~/Scripts/js/jquery.sticky.js",
                      "~/Scripts/js/jquery.scrollUp.min.js",
                      "~/Scripts/js/scrollbar/jquery.mCustomScrollbar.concat.min.js",
                      "~/Scripts/js/scrollbar/mCustomScrollbar-active.js",
                      "~/Scripts/js/metisMenu/metisMenu.min.js",
                      "~/Scripts/s/metisMenu/metisMenu-active.js",
                      "~/Scripts/js/sparkline/jquery.sparkline.min.js",
                      "~/Scripts/js/sparkline/jquery.charts-sparkline.js",
                      "~/Scripts/js/calendar/moment.min.js",
                      "~/Scripts/js/calendar/fullcalendar.min.js",
                      "~/Scripts/js/calendar/fullcalendar-active.js",
                      "~/Scripts/js/plugins.js",
                      "~/Scripts/js/main.js",
                      "~/Scripts/js/flot/jquery.flot.js",
                      "~/Scripts/js/flot/jquery.flot.resize.js",
                      "~/Scripts/js/flot/curvedLines.js",
                      "~/Scripts/js/flot/flot-active.js",
                      "~/Scripts/datatables/jquery.dataTables.min.js",
                      "~/Scripts/datatables/dataTables.buttons.min.js",
                     "~/Scripts/datatables/buttons.flash.min.js",
                      "~/Scripts/datatables/jszip.min.js",
                      "~/Scripts/datatables/pdfmake.min.js",
                      "~/Scripts/datatables/vfs_fonts.js",
                      "~/Scripts/datatables/buttons.html5.min.js",
                      "~/Scripts/datatables/buttons.print.min.js",
                      "~/Scripts/datatables/dataTables.bootstrap.js",
                      "~/Scripts/datepicker/dist/js/bootstrap-datepicker.min.js",
                      "~/Scripts/Datetimepicker/bootstrap-datetimepicker.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/nalika-icon.css",
                      "~/Content/css/owl.carousel.css",
                      "~/Content/css/owl.theme.css",
                      "~/Content/css/owl.transitions.css",
                      "~/Content/css/animate.css",
                      "~/Content/css/normalize.css",
                      "~/Content/css/meanmenu.min.css",
                      "~/Content/css/main.css",
                      "~/Content/css/morrisjs/morris.css",
                      "~/Content/css/scrollbar/jquery.mCustomScrollbar.min.css",
                      "~/Content/css/metisMenu/metisMenu.min.css",
                      "~/Content/css/metisMenu/metisMenu-vertical.css",
                      "~/Content/css/calendar/fullcalendar.min.css",
                      "~/Content/css/calendar/fullcalendar.print.min.css",
                      "~/Content/css/style.css",
                      "~/Content/css/responsive.css",
                     "~/Scripts/datatables/jquery.dataTables.min.css",
                     "~/Scripts/datatables/buttons.dataTables.min.css",
                     "~/Scripts/datepicker/dist/css/bootstrap-datepicker.min.css",
                     "~/Scripts/datepicker/dist/css/bootstrap-datepicker.standalone.min.css",
                     "~/Scripts/Datetimepicker/bootstrap-datetimepicker.css"
                      ));
        }
    }
}

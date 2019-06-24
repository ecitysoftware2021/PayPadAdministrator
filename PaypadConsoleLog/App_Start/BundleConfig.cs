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
                      "~/Scripts/jquery-ui/jquery-ui.min.js",
                      "~/Scripts/bootstrap/dist/js/bootstrap.min.js",
                      "~/Scripts/raphael/raphael.min.js",
                      "~/Scripts/morris.js/morris.min.js",
                      "~/Scripts/jquery-sparkline/dist/jquery.sparkline.min.js",
                      "~/Scripts/jquery-knob/dist/jquery.knob.min.js",
                      "~/Scripts/moment/min/moment.min.js",                                         
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
                      "~/Scripts/Datetimepicker/bootstrap-datetimepicker.js",
                      "~/Scripts/js/adminlte.min.js"                                            
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Scripts/bootstrap/dist/css/bootstrap.min.css",
                "~/Scripts/font-awesome/css/font-awesome.min.css",
                    "~/Scripts/Ionicons/css/ionicons.min.css",
                    "~/Content/css/AdminLTE.css",
                    "~/Content/css/skins/_all-skins.min.css",
                    "~/Scripts/morris.js/morris.css",
                     "~/Scripts/datatables/jquery.dataTables.min.css",
                     "~/Scripts/datatables/buttons.dataTables.min.css",
                     "~/Scripts/datepicker/dist/css/bootstrap-datepicker.min.css",
                     "~/Scripts/datepicker/dist/css/bootstrap-datepicker.standalone.min.css",
                     "~/Scripts/Datetimepicker/bootstrap-datetimepicker.css"
                      ));
        }
    }
}

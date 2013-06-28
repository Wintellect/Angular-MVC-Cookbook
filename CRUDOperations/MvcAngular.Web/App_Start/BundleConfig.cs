using System.Web;
using System.Web.Optimization;

namespace MvcAngular.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use Bundle rather than StyleBundle or ScriptBundle in order to turn off
            // minification (takes the already minified files).

            // CSS Bundles

            bundles.Add(new Bundle("~/Content/files/css-one")
                .Include("~/Content/bootstrap/bootstrap.css"));

            bundles.Add(new Bundle("~/Content/files/css-two")
                .Include("~/Content/bootstrap/bootstrap-responsive.css")
                .Include("~/Content/font-awesome/font-awesome.css")
                .Include("~/Content/app/main.css"));


            // Script Bundles

            bundles.Add(new Bundle("~/bundles/files/modernizr")
                .Include("~/Scripts/bootstrap/modernizr-2.6.2-respond-1.1.0.js"));

            bundles.Add(new Bundle("~/bundles/files/scripts")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/bootstrap/bootstrap.js")
                .Include("~/Scripts/angular/angular.js"));

            bundles.Add(new Bundle("~/bundles/files/alt-scripts")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/bootstrap/bootstrap.js"));
        }
    }
}
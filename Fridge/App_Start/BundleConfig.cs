using System.Web.Optimization;

namespace Fridge
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/ng").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-resource.js",
                "~/app/constants.js",
                "~/app/controllers/*.js",
                "~/app/app.js",
                "~/app/services/*.js"));
        }
    }
}
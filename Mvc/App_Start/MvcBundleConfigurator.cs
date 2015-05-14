using System.Web.Optimization;

namespace Yangaroo.App_Start
{
    static class MvcBundleConfigurator
    {
        public static void Configure(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/CupcakeScripts")
                .IncludeDirectory("~/Scripts/Angular", "*.js")
                .IncludeDirectory("~/Scripts", "*.js")
                //.IncludeDirectory("~/Scripts/i18n", "*.js")
                .IncludeDirectory("~/Scripts/Controllers", "*.js"));

            bundles.Add(CreateLayoutBundle());
        }

        private static Bundle CreateLayoutBundle()
        {
            return new LessBundle("~/bundles/LayoutBundle")
                .Include("~/Content/Styles/layout.less");
        }
    }
}
using System.Web.Optimization;

namespace AngularJSProofofConcept.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/App")
                .IncludeDirectory("~/Scripts/Angular", "*.js")
                .IncludeDirectory("~/Scripts", "*.js")
                //.IncludeDirectory("~/Scripts/i18n", "*.js")
                .IncludeDirectory("~/Scripts/Controllers", "*.js"));
        }
    }
}
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Yangaroo.Core;

namespace Yangaroo.App_Start
{
    static class MvcConfigurator
    {
        public static void Configure()
        {
            ConfigureRoutes();

            MvcBundleConfigurator.Configure(BundleTable.Bundles);
        }

        private static void ConfigureRoutes()
        {
            var artezSessionFactory = DependencyResolver.Current.GetService<IYangarooSessionFactory>();

            var routeConfigurator = new MvcRouteConfigurator(artezSessionFactory);

            routeConfigurator.Configure(RouteTable.Routes);
        }
    }
}
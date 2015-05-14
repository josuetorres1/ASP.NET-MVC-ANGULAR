using System.Collections.Generic;
using Yangaroo.Core;
using System.Web.Mvc;
using System.Web.Routing;
using Yangaroo.Routing;
using Yangaroo.Web.Handlers;

namespace Yangaroo.App_Start
{
    public class MvcRouteConfigurator
    {
        private const string DefaultController = "Cupcake";
        private const string DefaultAction = "Index";

        private readonly IYangarooSessionFactory _artezSessionFactory;

        public MvcRouteConfigurator(IYangarooSessionFactory artezSessionFactory)
        {
            _artezSessionFactory = artezSessionFactory;
        }

        public void Configure(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            ConfigureRegistrationAreaRoutes(routes);
        }

        private void ConfigureRegistrationAreaRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                null,
                "RouteCupCakes",
                new { controller = DefaultController, action = "RouteCupCakes" }
            );

            routes.MapRoute(
                "RouteCupCake",
                "RouteCupCake",
                new { controller = DefaultController, action = "RouteCupCake" }
            );

            routes.MapRoute(
                null,
                "{controller}/Insert/{id}",
                new { controller = DefaultController, action = "Insert", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "CupCakes",
                "{controller}/{action}/{id}",
                new { Controller = DefaultController, Action = DefaultAction, id = UrlParameter.Optional });
        }
    }
}
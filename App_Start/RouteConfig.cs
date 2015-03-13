using System.Web.Mvc;
using System.Web.Routing;

namespace AngularJSProofofConcept
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "AppC", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: null,
                url: "InsertEvent",
                defaults: new { controller = "AppC", action = "InsertEvent", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: null,
                url: "RouteEvent",
                defaults: new { controller = "AppC", action = "RouteEvent", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Api",
                url: "{controller}/{action}/{id}",
                defaults: new { Controller = "AppC", action = "Api", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: null,
                url: "RouteEvents",
                defaults: new { Controller = "AppC", action = "RouteEvents" }
            );
        }
    }
}
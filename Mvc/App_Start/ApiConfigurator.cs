using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;
using System.Web.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Yangaroo.Web.Filters;
using Yangaroo.Web.Handlers;
using Yangaroo.Web.Http;

namespace Yangaroo.App_Start
{
    public class ApiConfigurator
    {
        public static void Configure(HttpConfiguration configuration)
        {
            MapRoutes(configuration.Routes);

            ConfigureFormatters(configuration.Formatters);

            ConfigureFilters(configuration.Filters, configuration.DependencyResolver);

            ConfigureMessageHandlers(configuration.MessageHandlers);
        }

        private static void MapRoutes(HttpRouteCollection httpRouteCollection)
        {
            httpRouteCollection.MapHttpRoute(
                null,
                "Api/{controller}/Post/{id}",
                new { action = "Post", id = RouteParameter.Optional },
                new { httpMethod = new HttpMethodConstraint("POST") });

            httpRouteCollection.MapHttpRoute(
                null,
                "Api/{controller}/Delete/{id}",
                new { action = "Delete", id = RouteParameter.Optional },
                new { httpMethod = new HttpMethodConstraint("POST") });

            httpRouteCollection.MapHttpRoute(
                null,
                "Api/{controller}/PostCupCake/{id}",
                new { action = "PostCupCake", id = RouteParameter.Optional },
                new { httpMethod = new HttpMethodConstraint("POST") });

            httpRouteCollection.MapHttpRoute(
                "CupCakeWebApi",
                "Api/{controller}/{id}",
                new { id = RouteParameter.Optional },
                new { httpMethod = new HttpMethodConstraint("GET") });
        }

        private static void ConfigureFormatters(MediaTypeFormatterCollection formatters)
        {
            formatters.Remove(formatters.XmlFormatter);

            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            #if DEBUG
            settings.Formatting = Formatting.Indented;
            #endif
        }

        private static void ConfigureFilters(HttpFilterCollection httpFilterCollection, IDependencyResolver dependencyResolver)
        {
            httpFilterCollection.Add(dependencyResolver.GetService<UnhandledExceptionFilterAttribute>());
        }

        private static void ConfigureMessageHandlers(ICollection<DelegatingHandler> messageHandlers)
        {
            messageHandlers.Add(new AcceptLanguageHandler());
        }
    }
}
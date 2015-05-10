using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using NLog.Interface;

namespace Yangaroo.Web.Filters
{
    public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public UnhandledExceptionFilterAttribute(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnException(HttpActionExecutedContext context)
        {
           _logger.Error("Unhandled Exception", context.Exception);

            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}

using System.Net;
using System.Web;
using System.Web.Routing;

namespace Yangaroo.Routing
{
    public class NotFoundHttpHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            throw new HttpException((int)HttpStatusCode.NotFound, "Path '" + context.Request.Path + "' was not found.");
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
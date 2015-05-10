using System.Web.Http;
using Yangaroo.App_Start;

namespace Yangaroo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ApiConfigurator.Configure(GlobalConfiguration.Configuration);
            MvcConfigurator.Configure();
        }
    }
}
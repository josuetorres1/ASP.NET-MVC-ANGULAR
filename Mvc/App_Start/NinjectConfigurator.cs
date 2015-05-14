using System.Web.Http;
using Yangaroo.Web.DependencyInjection;
using Fundraising.App_Start;
using Ninject.Web.Common;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectConfigurator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectConfigurator), "Stop")]

namespace Fundraising.App_Start
{
    internal static class NinjectConfigurator
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static void Start()
        {
            Bootstrapper.Initialize(new NinjectKernelFactory().Create);

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDepemdencyResolver(Bootstrapper.Kernel);
        }

        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
    }
}
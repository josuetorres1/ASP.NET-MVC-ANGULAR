using System.Configuration;
using Yangaroo.Core;
using Ninject;
using Ninject.Activation;
using Ninject.Web.Common;

namespace Yangaroo.Web.DependencyInjection
{
    public static class UnitOfWorkDependencies
    {
        public static void BindToKernel(IKernel kernel)
        {
            kernel.Bind<IYangarooSessionFactory>().ToMethod(CreateMsSqlSessionFactory).InSingletonScope();
            kernel.Bind<IYangarooSession>().ToMethod(CreateYangarooSession).InRequestScope();
            kernel.Bind<IUnitOfWorkFactory>().ToMethod(CreateUnitOfWorkFactory);
        }

        private static IYangarooSessionFactory CreateMsSqlSessionFactory(IContext context)
        {
            return new MsSqlSessionFactory(ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString);   
        }

        private static IYangarooSession CreateYangarooSession(IContext context)
        {
            return context.Kernel.Get<IYangarooSessionFactory>().Create();
        }

        private static IUnitOfWorkFactory CreateUnitOfWorkFactory(IContext context)
        {
            return context.Kernel.Get<IYangarooSession>().CreateUnitOfWorkFactory();
        }
    }
}

using Yangaroo.Core.Events;
using Yangaroo.Web.DependencyInjection;
using Yangaroo.Web.Events;
using Ninject;

namespace Fundraising.App_Start
{
    internal class NinjectKernelFactory : NinjectKernelFactoryTemplate
    {
        protected override void RegisterServices(IKernel kernel)
        {
            UnitOfWorkDependencies.BindToKernel(kernel);

            kernel.Bind<IYangarooDependencyResolver>().ToMethod(ctx => new NinjectYangarooDependencyResolver(kernel));
        }
    }
}
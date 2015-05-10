using Ninject;
using System.Web.Http.Dependencies;

namespace Yangaroo.Web.DependencyInjection
{
    public class NinjectDepemdencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDepemdencyResolver(IKernel kernel) 
            : base(kernel, false)
        {
            _kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(_kernel.BeginBlock(), true);
        }
    }
}

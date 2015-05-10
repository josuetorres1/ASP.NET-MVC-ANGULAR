using System;
using System.Collections.Generic;
using Ninject;
using Yangaroo.Core.Events;

namespace Yangaroo.Web.Events
{
    public class NinjectYangarooDependencyResolver : IYangarooDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectYangarooDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}

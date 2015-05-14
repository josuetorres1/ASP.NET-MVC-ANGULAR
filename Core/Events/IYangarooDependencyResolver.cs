using System;
using System.Collections.Generic;

namespace Yangaroo.Core.Events
{
    public interface IYangarooDependencyResolver
    {
        IEnumerable<object> GetServices(Type serviceType);
    }
}

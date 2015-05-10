using System;
using System.Web.Http.Dependencies;

namespace Yangaroo.Web.Http
{
    public static class DependencyScopeExtensions
    {
        public static T GetService<T>(this IDependencyScope dependencyScope) where T : class
        {
            if (dependencyScope == null)
            {
                throw new ArgumentNullException("dependencyScope");
            }

            return (T)dependencyScope.GetService(typeof(T));
        }
    }
}

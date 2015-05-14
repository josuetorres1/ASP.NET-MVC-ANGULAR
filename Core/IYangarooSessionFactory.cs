using System;

namespace Yangaroo.Core
{
    public interface IYangarooSessionFactory : IDisposable
    {
        IYangarooSession Create();
    }
}

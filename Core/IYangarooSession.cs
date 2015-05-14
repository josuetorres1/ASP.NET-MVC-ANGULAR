using System;

namespace Yangaroo.Core
{
    public interface IYangarooSession : IDisposable
    {
        IUnitOfWorkFactory CreateUnitOfWorkFactory();
    }
}

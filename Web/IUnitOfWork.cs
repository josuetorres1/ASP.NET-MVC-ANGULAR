using System;
using Yangaroo.Core.Repositories;

namespace Yangaroo.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICupCakeRepository CupCakeRepository { get; }
    }
}

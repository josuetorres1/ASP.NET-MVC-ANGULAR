using System;
using Yangaroo.Core;

namespace Yangaroo.Core
{
    public class YangarooSession : IYangarooSession
    {
        private readonly string _connectionString;

        public IUnitOfWorkFactory CreateUnitOfWorkFactory()
        {
            return new YangarooUnitOfWorkFactory(_connectionString);
        }

        public YangarooSession(string connectionString )
        {
            _connectionString = connectionString;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}

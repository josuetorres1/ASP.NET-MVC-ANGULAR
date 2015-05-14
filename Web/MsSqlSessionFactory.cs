using System;

namespace Yangaroo.Core
{
    public class MsSqlSessionFactory : IYangarooSessionFactory
    {
        private readonly string _connectionString ;

        public IYangarooSession Create()
        {
            return new YangarooSession(_connectionString);
        }

        public MsSqlSessionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}

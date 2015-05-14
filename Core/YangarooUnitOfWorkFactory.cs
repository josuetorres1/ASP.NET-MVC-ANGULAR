using Yangaroo.Core;

namespace Yangaroo.Core
{
    public class YangarooUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly string _connectionString;

        public IUnitOfWork Create()
        {
            return new YangarooUnitOfWork(_connectionString);
        }

        public YangarooUnitOfWorkFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}

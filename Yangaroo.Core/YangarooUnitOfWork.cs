using System;
using Yangaroo.Core;
using Yangaroo.Core.Repositories;

namespace Yangaroo.Core
{
    public class YangarooUnitOfWork : IUnitOfWork
    {
        private readonly Lazy<ICupCakeRepository> _lazyCupCakeRepository;
        private readonly Lazy<IDatabaseHelper> _lazyDatabaseHelper;
        private readonly string _connectionString;

        public YangarooUnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
            _lazyCupCakeRepository = new Lazy<ICupCakeRepository>(() => new CupCakeRepository(_connectionString));
            _lazyDatabaseHelper = new Lazy<IDatabaseHelper>(() => new DatabaseHelper(_connectionString));
        }

        public ICupCakeRepository CupCakeRepository
        {
            get { return _lazyCupCakeRepository.Value; }
        }

        public IDatabaseHelper DatabaseHelper
        {
            get { return _lazyDatabaseHelper.Value; }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}

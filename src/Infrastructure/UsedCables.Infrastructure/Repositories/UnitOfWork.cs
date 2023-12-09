using UsedCables.Infrastructure.Entities;
using UsedCables.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace UsedCables.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new();
        private bool _disposed;

        public Dictionary<Type, object> Repositories
        {
            get { return _repositories; }
            set { Repositories = value; }
        }

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IAsyncRepository<T> Repository<T>() where T : EntityBase
        {
            if (Repositories.ContainsKey(typeof(T)))
            {
                return Repositories[typeof(T)] as IAsyncRepository<T>;
            }

            IAsyncRepository<T> repo = new AsyncRepository<T>(_dbContext);
            Repositories.Add(typeof(T), repo);
            return repo;
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
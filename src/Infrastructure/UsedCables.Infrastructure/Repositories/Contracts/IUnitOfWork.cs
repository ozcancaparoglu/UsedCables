using UsedCables.Infrastructure.Entities;

namespace UsedCables.Infrastructure.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IAsyncRepository<T> Repository<T>() where T : EntityBase;

        Task<int> CommitAsync();

        void Rollback();
    }
}
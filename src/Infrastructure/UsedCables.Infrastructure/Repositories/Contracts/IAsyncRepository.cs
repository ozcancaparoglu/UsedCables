using UsedCables.Infrastructure.Entities;
using System.Linq.Expressions;

namespace UsedCables.Infrastructure.Repositories.Contracts
{
    public interface IAsyncRepository<T> where T : EntityBase
    {
        Task Add(T entity);

        Task BulkDelete(IList<T> entities);

        Task BulkInsert(IList<T> entities);

        Task BulkUpdate(IList<T> entities);

        Task<int> Count();

        Task<int> CountExpression(Expression<Func<T, bool>> predicate);

        Task<ICollection<T>> Filter(Expression<Func<T, bool>> filter, int? page = null, int? pageSize = null);

        Task<ICollection<T>> FilterWithProperties(Expression<Func<T, bool>>? filter = null,
           string includeProperties = "", Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           int? page = null, int? pageSize = null);

        Task<T> Find(Expression<Func<T, bool>> match);

        Task<T> FindByProperties(Expression<Func<T, bool>> match, string includeProperties = "");

        Task<ICollection<T>> GetAll();

        Task<T> GetById(int id);

        IQueryable<T> Table();

        T Update(T entity);

        void Delete(T entity);

        Task<int> ExecuteCommand(string sql, params object[] parameters);
    }
}
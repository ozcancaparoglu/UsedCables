using UsedCables.Infrastructure.Entities;
using UsedCables.Infrastructure.Repositories.Contracts;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UsedCables.Infrastructure.Repositories
{
    public class AsyncRepository<T> : IAsyncRepository<T> where T : EntityBase
    {
        protected readonly DbContext _dbContext;

        public AsyncRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual async Task<int> ExecuteCommand(string sql, params object[] parameters) => await _dbContext.Database.ExecuteSqlRawAsync(sql, parameters);

        public virtual IQueryable<T> Table() => _dbContext.Set<T>().AsQueryable();

        public virtual async Task<ICollection<T>> GetAll() => await _dbContext.Set<T>().ToListAsync();

        public virtual async Task<T> GetById(int id) => await _dbContext.Set<T>().FindAsync(id);

        public virtual async Task<T> Find(Expression<Func<T, bool>> match) => await _dbContext.Set<T>().SingleOrDefaultAsync(match);

        public virtual async Task<T> FindByProperties(Expression<Func<T, bool>> match, string includeProperties = "")
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (match != null)
                query = query.Where(match);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return await query.SingleOrDefaultAsync();
        }

        public virtual async Task<ICollection<T>> Filter(Expression<Func<T, bool>> filter, int? page = null, int? pageSize = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            query = query.Where(filter);

            if (page != null && pageSize != null)
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return await query.ToListAsync();
        }

        public virtual async Task<ICollection<T>> FilterWithProperties(Expression<Func<T, bool>>? filter = null,
           string includeProperties = "", Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           int? page = null, int? pageSize = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return await query.ToListAsync();
        }

        public virtual async Task Add(T entity) => await _dbContext.Set<T>().AddAsync(entity);

        public virtual T Update(T entity)
        {
            var entityEntry = _dbContext.Set<T>().Update(entity);
            return entityEntry.Entity;
        }

        public virtual async Task<int> Count() => await _dbContext.Set<T>().CountAsync();

        public virtual async Task<int> CountExpression(Expression<Func<T, bool>> predicate) => await _dbContext.Set<T>().Where(predicate).CountAsync();

        public virtual async Task BulkUpdate(IList<T> entities) => await _dbContext.BulkInsertOrUpdateAsync(entities);

        public virtual async Task BulkInsert(IList<T> entities) => await _dbContext.BulkInsertAsync(entities);

        public virtual async Task BulkDelete(IList<T> entities) => await _dbContext.BulkDeleteAsync(entities);

        public virtual void Delete(T entity) => _dbContext.Set<T>().Remove(entity);
    }
}
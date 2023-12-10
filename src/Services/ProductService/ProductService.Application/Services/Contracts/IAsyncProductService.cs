using ProductService.Domain.ProductAggregate;
using UsedCables.Infrastructure.Helpers.ResponseHelper;

namespace ProductService.Application.Services.Contracts
{
    public interface IAsyncProductService
    {
        #region Crud Methods
        Task AddAsync(Product product);
        Task BulkDeleteAsync(IEnumerable<Product> products);
        Task BulkInsertAsync(IEnumerable<Product> products);
        Task BulkUpdateAsync(IEnumerable<Product> products);
        Task DeleteAsync(int id);
        Task<Product?> GetByIdAsync(int id);
        Task<PagerOutput<Product>> GetPagedAsync(PagerInput pagerInput);
        Task UpdateAsync(Product product);
        #endregion Crud Methods
    }
}
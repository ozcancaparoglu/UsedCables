using ProductService.Domain.ProductAggregate;
using UsedCables.Infrastructure.Helpers.ResponseHelper;

namespace ProductService.Application.Services.Contracts
{
    public interface IAsyncProductService
    {
        #region Crud Methods
        Task<Guid> CreateAsync(Product product);
        Task BulkDeleteAsync(IEnumerable<Product> products);
        Task BulkInsertAsync(IEnumerable<Product> products);
        Task BulkUpdateAsync(IEnumerable<Product> products);
        Task DeleteAsync(Guid id);
        Task<Product?> GetByIdAsync(Guid id);
        Task<PagerOutput<Product>> GetPagedAsync(PagerInput pagerInput);
        Task<Guid> UpdateAsync(Product product);
        #endregion Crud Methods

        #region Custom Methods
        Task<PagerOutput<Product>> GetProductsWithParentIdAsync(Guid parentId);
        Task<Product> GetCompleteProductAsync(Guid id);
        Task<Product?> GetCompleteProductJoinAsync(Guid id);
        #endregion Custom Methods
    }
}
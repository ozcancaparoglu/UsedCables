using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using ProductService.Application.Services.Contracts;
using ProductService.Domain.ProductAggregate;
using UsedCables.Infrastructure.Configuration;
using UsedCables.Infrastructure.Enums;
using UsedCables.Infrastructure.Helpers.ResponseHelper;
using UsedCables.Infrastructure.Repositories.Contracts;

namespace ProductService.Application.Services
{
    public class AsyncProductService : IAsyncProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AsyncProductService> _logger;
        private readonly IOptions<ConnectionStrings> _settings;
        private readonly string _connectionString;

        public AsyncProductService(IUnitOfWork unitOfWork, ILogger<AsyncProductService> logger, IOptions<ConnectionStrings> settings)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _connectionString = _settings.Value.AppConnectionString;
        }

        #region Crud Methods

        public async Task<PagerOutput<Product>> GetPagedAsync(PagerInput pagerInput)
        {
            using (NpgsqlConnection connection = new(_connectionString))
            {
                await connection.OpenAsync();

                string query = @$"SELECT * FROM ""Products"" WHERE ""State"" = @State LIMIT {pagerInput.Take} OFFSET {pagerInput.Skip};
                                  SELECT COUNT(*) FROM ""Products"" WHERE ""State"" = @State";

                using (var multi = await connection.QueryMultipleAsync(query, new { State = (int)State.Active }))
                {
                    var products = await multi.ReadAsync<Product>();
                    var total = await multi.ReadFirstAsync<int>();

                    return new PagerOutput<Product>(products, total);
                }
            }
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            using (NpgsqlConnection connection = new(_connectionString))
            {
                await connection.OpenAsync();

                string query = @$"SELECT * FROM ""Products"" WHERE ""Id"" = @Id AND ""State"" = @State";

                return await connection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id, State = (int)State.Active });
            }
        }

        public async Task<int> CreateAsync(Product product)
        {
            await _unitOfWork.Repository<Product>().Add(product);

            await _unitOfWork.CommitAsync();

            _logger.LogInformation($"Product with id: {product.Id} added.");

            return product.Id;
        }

        public async Task<int> UpdateAsync(Product product)
        {
            _unitOfWork.Repository<Product>().Update(product);

            await _unitOfWork.CommitAsync();

            _logger.LogInformation($"Product with id: {product.Id} updated.");

            return product.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogError($"Product with id: {id} not found.");
                return;
            }

            entity.Deleted();

            _unitOfWork.Repository<Product>().Update(entity);

            await _unitOfWork.CommitAsync();

            _logger.LogInformation($"Product with id: {entity.Id} deleted.");
        }

        public async Task BulkInsertAsync(IEnumerable<Product> products) => await _unitOfWork.Repository<Product>().BulkInsert(products.ToList());

        public async Task BulkUpdateAsync(IEnumerable<Product> products) => await _unitOfWork.Repository<Product>().BulkUpdate(products.ToList());

        public async Task BulkDeleteAsync(IEnumerable<Product> products) => await _unitOfWork.Repository<Product>().BulkDelete(products.ToList());

        #endregion Crud Methods

        #region Custom Methods

        public async Task<PagerOutput<Product>> GetProductsWithParentIdAsync(int parentId)
        {
            using (NpgsqlConnection connection = new(_connectionString))
            {
                await connection.OpenAsync();

                string query = @$"SELECT * FROM ""Products"" WHERE ""ParentId"" = @ParentId AND ""State"" = @State;
                                  SELECT COUNT(*) FROM ""Products"" WHERE ""ParentId"" = @ParentId AND ""State"" = @State";

                using (var multi = await connection.QueryMultipleAsync(query, new { ParentId = parentId, State = (int)State.Active }))
                {
                    var products = await multi.ReadAsync<Product>();
                    var total = await multi.ReadFirstAsync<int>();

                    return new PagerOutput<Product>(products, total);
                }
            }
        }

        public async Task<Product> GetCompleteProductAsync(int id)
        {
            using (NpgsqlConnection connection = new(_connectionString))
            {
                await connection.OpenAsync();
                //TODO: Böyle daha stabil olabilir. Hız konusuna bakılmalı.
                string query = @$"SELECT * FROM ""Products"" WHERE ""Id"" = @Id AND ""State"" = @State;
                                  SELECT * FROM ""ProductPictures"" WHERE ""ProductId"" = @Id AND ""State"" = @State;
                                  SELECT * FROM ""ProductAttributes"" WHERE ""ProductId"" = @Id AND ""State"" = @State;
                                  SELECT * FROM ""ProductSellers"" WHERE ""ProductId"" = @Id AND ""State"" = @State";

                using (var multi = await connection.QueryMultipleAsync(query, new { Id = id, State = (int)State.Active }))
                {
                    var product = await multi.ReadFirstAsync<Product>();
                    var productPictures = await multi.ReadAsync<ProductPictures>();
                    var productAttributes = await multi.ReadAsync<ProductAttributes>();
                    var productSellers = await multi.ReadAsync<ProductSellers>();

                    product.SetProductPictures(productPictures.ToList());
                    product.SetProductAttributes(productAttributes.ToList());
                    product.SetProductSellers(productSellers.ToList());

                    return product;
                }
            }
        }

        public async Task<Product?> GetCompleteProductJoinAsync(int id)
        {
            using (NpgsqlConnection connection = new(_connectionString))
            {
                await connection.OpenAsync();

                string query = @$"SELECT * FROM ""Products"" AS p
                                  LEFT JOIN ""ProductPictures"" AS pp ON p.""Id"" = pp.""ProductId""
                                  LEFT JOIN ""ProductAttributes"" AS pa ON p.""Id"" = pa.""ProductId""
                                  LEFT JOIN ""ProductSellers"" AS ps ON p.""Id"" = ps.""ProductId""
                                  WHERE p.""Id"" = @Id AND p.""State"" = @State";

                var product = await connection.QueryAsync<Product, ProductPictures, ProductAttributes, ProductSellers, Product>(query,
                    (p, pp, pa, ps) =>
                    {
                        p.SetProductPictures([pp]);
                        p.SetProductAttributes([pa]);
                        p.SetProductSellers([ps]);
                        return p;
                    }, new { Id = id, State = (int)State.Active }, splitOn: "Id, Id, Id");

                return product.FirstOrDefault();
            }
        }

        #endregion Custom Methods
    }
}
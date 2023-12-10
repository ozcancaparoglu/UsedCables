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

        public async Task<IEnumerable<Product>> GetAllAsync(PagerInput pagerInput)
        {
            using (NpgsqlConnection connection = new(_connectionString))
            {
                await connection.OpenAsync();

                string query = @$"SELECT * FROM ""Products"" WHERE ""State"" = @State LIMIT {pagerInput.Take} OFFSET {pagerInput.Skip}";

                return await connection.QueryAsync<Product>(query, new { State = (int)State.Active });
            }
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            using (NpgsqlConnection connection = new(_connectionString))
            {
                await connection.OpenAsync();

                string query = @$"SELECT * FROM ""Products"" WHERE ""Id"" = @Id AND ""State"" = @State";

                return await connection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id, State = (int)State.Active });
            }
        }
    }
}
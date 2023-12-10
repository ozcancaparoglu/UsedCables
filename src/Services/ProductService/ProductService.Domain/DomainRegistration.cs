using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Domain.Persistence;
using UsedCables.Infrastructure.Cache.Redis;
using UsedCables.Infrastructure.Configuration;

namespace ProductService.Domain
{
    public static class DomainRegistration
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            //For Dapper
            services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));

            //For EF Core
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("AppConnectionString")));

            services.Configure<RedisConfigurationOptions>(configuration.GetSection("RedisDatabase"));

            services.AddScoped<DbContext, AppDbContext>();

            return services;
        }
    }
}
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsedCables.Infrastructure.Configuration;

namespace Authentication.Infrastructure
{
    public static class AuthenticationRegistration
    {
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfigurationOptions>(configuration.GetSection("JwtConfiguration"));

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("AppConnectionString")));

            services.AddScoped<DbContext, AppDbContext>();

            return services;
        }
    }
}
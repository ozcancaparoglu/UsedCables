using Authentication.Infrastructure.Models;
using Authentication.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
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

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<RoleManager<IdentityRole>>();

            return services;
        }
    }
}
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using UsedCables.Infrastructure.Behaviours;
using UsedCables.Infrastructure.Cache;
using UsedCables.Infrastructure.Cache.Redis;
using UsedCables.Infrastructure.Helpers.RestHelper;
using UsedCables.Infrastructure.Middlewares;
using UsedCables.Infrastructure.Repositories;
using UsedCables.Infrastructure.Repositories.Contracts;

namespace UsedCables.Infrastructure.Ioc
{
    public static class InfrastructureModule
    {
        public static IServiceCollection InfrastructureServices(this IServiceCollection services)
        {
            #region Middlewares

            services.AddTransient<ExceptionHandlingMiddleware>();

            #endregion Middlewares

            #region Repositories

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion Repositories

            #region Validators

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            #endregion Validators

            #region Redis

            services.AddScoped<ICacheService, RedisCacheService>();
            services.AddScoped<RedisServer>();

            #endregion Redis

            #region Helpers

            services.AddScoped<IRestClientHelper, RestClientHelper>();

            #endregion Helpers

            return services;
        }
    }
}
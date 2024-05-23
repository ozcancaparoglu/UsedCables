using Authentication.Infrastructure.Features.HealthCheck;
using Autofac;
using MediatR;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Authentication.Infrastructure
{
    public class Bootstrapper : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(HealthCheckQuery).Assembly)
                .Where(type => !type.IsAssignableTo<IHostedService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(HealthCheckQueryHandler).Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(Bootstrapper).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(Bootstrapper).Assembly)
                .Where(t => t.Name.EndsWith("Communicator"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

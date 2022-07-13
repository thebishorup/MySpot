using Microsoft.Extensions.DependencyInjection;

using MySpot.Application.Abstractions;

namespace MySpot.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // automatically detech command hadlers to register into IOC container
            var applicationAssembly = typeof(ICommandHandler<>).Assembly;
            
            services.Scan(s =>
            s.FromAssemblies(applicationAssembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}

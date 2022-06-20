using Microsoft.Extensions.DependencyInjection;

using MySpot.Application.Abstractions;
using MySpot.Application.Services;

namespace MySpot.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IReservationService, ReservationService>();

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

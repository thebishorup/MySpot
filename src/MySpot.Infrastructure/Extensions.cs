using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MySpot.Core.Abstractions;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.Exceptions;
using MySpot.Infrastructure.Time;

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MySpot.Tests.Unit")]
namespace MySpot.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            //var appOptions = configuration.GetOptions<AppOptions>("app");
            //services.AddSingleton(appOptions);
            services.Configure<AppOptions>(configuration.GetRequiredSection("app"));

            services.AddSingleton<ExceptionMiddleware>();

            services
                .AddPostgres(configuration)
                .AddSingleton<IClock, Clock>();

            return services;
        }

        public static WebApplication UseInfrastructure(this WebApplication app)
        {
            // register middlewares
            app.UseMiddleware<ExceptionMiddleware>();

            app.MapControllers();

            return app;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) 
            where T : class, new()
        {
            var options = new T();
            var section = configuration.GetRequiredSection(sectionName);
            section.Bind(options);

            return options;
        }
    }
}

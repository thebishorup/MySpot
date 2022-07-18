using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using MySpot.Application.Abstractions;
using MySpot.Core.Abstractions;
using MySpot.Infrastructure.Auth;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.Exceptions;
using MySpot.Infrastructure.Logging;
using MySpot.Infrastructure.Security;
using MySpot.Infrastructure.Time;

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MySpot.Tests.Unit")]
[assembly: InternalsVisibleTo("MySpot.Tests.Integration")]
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
            services.AddHttpContextAccessor();

            services
                .AddPostgres(configuration)
                .AddSingleton<IClock, Clock>();

            services.AddCustomLogging();
            services.AddSecurity();
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MySpot API"
                });
            });

            // automatically detech queries hadlers to register into IOC container
            var infrastructureAssembly = typeof(AppOptions).Assembly;

            services.Scan(s =>
            s.FromAssemblies(infrastructureAssembly)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddAuth(configuration);

            return services;
        }

        public static WebApplication UseInfrastructure(this WebApplication app)
        {
            // register middlewares
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseSwagger();
            app.UseReDoc(reDoc =>
            {
                reDoc.RoutePrefix = "docs";
                reDoc.SpecUrl("/swagger/v1/swagger.json");
                reDoc.DocumentTitle = "MySpot API";
            });
            app.UseAuthentication();
            app.UseAuthorization();

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

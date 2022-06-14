using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repositories;

namespace MySpot.Infrastructure.DAL;
public static class Extensions
{
    private const string OptionsSectionName = "postgres";
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var postgrestOptions = configuration.GetOptions<PostgresOptions>(OptionsSectionName);

        services.AddDbContext<MySpotDbContext>(x =>
        { 
            x.UseNpgsql(postgrestOptions.ConnectionString);
            x.EnableSensitiveDataLogging();
        });

        services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
        services.AddHostedService<DatabaseInitilizer>();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }
}
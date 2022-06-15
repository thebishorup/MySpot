using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MySpot.Core.Abstractions;
using MySpot.Core.Entities;

namespace MySpot.Infrastructure.DAL;
internal sealed class DatabaseInitilizer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IClock _clock;

    // Service location "anti-pattern" -> Practical :)
    public DatabaseInitilizer(IServiceProvider serviceProvider, IClock clock)
    {
        _serviceProvider = serviceProvider;
        _clock = clock;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MySpotDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        // seed data
        if (await dbContext.WeeklyParkingSpots.AnyAsync(cancellationToken)) return;

        var weeklyParkingSpots = new List<WeeklyParkingSpot>()
            {
                WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000001"), new(_clock.Current()), "P1"),
                WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000002"), new(_clock.Current()), "P2"),
                WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000003"), new(_clock.Current()), "P3"),
                WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000004"), new(_clock.Current()), "P4"),
                WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000005"), new(_clock.Current()), "P5"),
            };

        await dbContext.WeeklyParkingSpots.AddRangeAsync(weeklyParkingSpots, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using MySpot.Core.Repositories;
using MySpot.Core.Abstractions;

namespace MySpot.Infrastructure.DAL.Repositories
{
    internal sealed class InMemoryWeeklyParkingSpot : IWeeklyParkingSpotRepository
    {
        private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;

        public InMemoryWeeklyParkingSpot(IClock clock)
        {
            _weeklyParkingSpots = new()
            {
                WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000001"), new(clock.Current()), "P1"),
                WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000002"), new(clock.Current()), "P2"),
                WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000003"), new(clock.Current()), "P3"),
                WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000004"), new(clock.Current()), "P4"),
                WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000005"), new(clock.Current()), "P5"),
            };
        }

        public async Task AddAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            await Task.CompletedTask;
            _weeklyParkingSpots.Add(weeklyParkingSpot);
        }

        public async Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id)
        {
            await Task.CompletedTask;
            return _weeklyParkingSpots.SingleOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync()
        {
            await Task.CompletedTask;
            return _weeklyParkingSpots;
        }

        public async Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}

using MySpot.Application.Command;

using Shouldly;

using System;

using Xunit;
using MySpot.Core.Repositories;
using MySpot.Application.Services;
using MySpot.Tests.Unit.Shared;
using MySpot.Infrastructure.DAL.Repositories;
using System.Threading.Tasks;
using MySpot.Core.Abstractions;

namespace MySpot.Tests.Unit.Services
{
    public class ReservationServiceTests
    {
        [Fact]
        public async Task given_valid_commands_should_create_reservation()
        {
            //ARRANGE
            var command = new ReserveParkingSpotForVehicle(
                Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Guid.NewGuid(), "Jane Doe",
                "XYZ 1122", _clock.Current().AddDays(1));

            //ACT
            var reservationId = await _reservationService.ReserveSpotForVehicleAsync(command);

            //ASSERT
            reservationId.ShouldNotBeNull();
            reservationId.Value.ShouldBe(command.ReservationId);
        }

        [Fact]
        public async Task given_invalid_parking_spot_id_should_fail()
        {
            //ARRANGE
            var command = new ReserveParkingSpotForVehicle(
                Guid.Parse("00000000-0000-0000-0000-000000000010"),
                Guid.NewGuid(), "Jane Doe",
                "XYZ 1122", DateTime.UtcNow.AddDays(1));

            //ACT
            var reservationId = await _reservationService.ReserveSpotForVehicleAsync(command);

            //ASSERT
            reservationId.ShouldBeNull();
        }

        [Fact]
        public async Task given_reservation_for_already_taken_date_create_should_fail()
        {
            //ARRANGE
            var command = new ReserveParkingSpotForVehicle(
                Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Guid.NewGuid(), "Jane Doe",
                "XYZ 1122", DateTime.UtcNow.AddDays(1));

            await _reservationService.ReserveSpotForVehicleAsync(command);

            //ACT
            var reservationId = await _reservationService.ReserveSpotForVehicleAsync(command);

            //ASSERT
            reservationId.ShouldBeNull();
        }

        #region ARRANGE

        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
        private readonly IClock _clock;
        private readonly IReservationService _reservationService;
        public ReservationServiceTests()
        {
            _clock = new ClockTest();
            _weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpot(_clock);
            _reservationService = new ReservationService(_clock, _weeklyParkingSpotRepository);
        }

        #endregion
    }
}

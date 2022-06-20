using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Abstractions;
using MySpot.Core.DomainServices;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Command.Handlers
{
    public sealed class ReserveParkingSpotForVehicleHandler : ICommandHandler<ReserveParkingSpotForVehicle>
    {
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
        private readonly IParkingReservationService _parkingReservationService;
        private readonly IClock _clock;

        public ReserveParkingSpotForVehicleHandler(IWeeklyParkingSpotRepository weeklyParkingSpotRepository, 
            IParkingReservationService parkingReservationService, IClock clock)
        {
            _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
            _parkingReservationService = parkingReservationService;
            _clock = clock;
        }

        public async Task HandleAsync(ReserveParkingSpotForVehicle command)
        {
            var (spotId, reservationId, employeeName, licensePlate, capacity, date) = command;

            var week = new Week(_clock.Current());
            var parkingSpotId = new ParkingSpotId(spotId);

            var weeklyParkingSpots =
                await _weeklyParkingSpotRepository.GetByWeekAsync(week);

            var parkingSportToReserver = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);

            if (parkingSportToReserver is null)
                throw new WeeklyParkingSpotNotFoundException(spotId);

            var reservation =
                new VehicleReservation(reservationId, employeeName, licensePlate, capacity, new Date(date));

            _parkingReservationService.ReserveSpotForVehicle(weeklyParkingSpots, JobTitle.Employee,
                parkingSportToReserver, reservation);

            await _weeklyParkingSpotRepository.UpdateAsync(parkingSportToReserver);
        }
    }
}

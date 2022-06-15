using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;
using MySpot.Application.Command;
using MySpot.Application.DTO;
using MySpot.Application.Exceptions;
using MySpot.Core.Abstractions;
using MySpot.Core.DomainServices;

namespace MySpot.Application.Services
{
    public sealed class ReservationService : IReservationService
    {
        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
        private readonly IParkingReservationService _parkingReservationService;

        public ReservationService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository, IParkingReservationService parkingReservationService)
        {
            _clock = clock;
            _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
            _parkingReservationService = parkingReservationService;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync() =>
            (await _weeklyParkingSpotRepository
            .GetAllAsync())
            .SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto
            {
                Id = x.Id,
                EmployeeName = x is VehicleReservation vr ? vr.EmployeeName : null,
                Date = x.Date.Value.Date,
            });

        public async Task<ReservationDto> GetAsync(Guid id) =>
            (await GetAllWeeklyAsync()).SingleOrDefault(x => x.Id == id);

        public async Task ReserveSpotForVehicleAsync(ReserveParkingSpotForVehicle command)
        {
            var (spotId, reservationId, employeeName, licensePlate, capacity, date) = command;

            var week = new Week(_clock.Current());
            var parkingSpotId = new ParkingSpotId(spotId);

            var weeklyParkingSpots =
                (await _weeklyParkingSpotRepository.GetByWeekAsync(week));

            var parkingSportToReserver = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);

            if (parkingSportToReserver is null)
                throw new WeeklyParkingSpotNotFoundException(spotId);

            var reservation =
                new VehicleReservation(reservationId, employeeName, licensePlate, capacity, new Date(date));

            _parkingReservationService.ReserveSpotForVehicle(weeklyParkingSpots, JobTitle.Employee,
                parkingSportToReserver, reservation);

            await _weeklyParkingSpotRepository.UpdateAsync(parkingSportToReserver);
        }

        public async Task ChangeReservedVehicleLicensePlateAsync(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotReservation(command.ReservationId);

            if (weeklyParkingSpot is null) 
                throw new WeeklyParkingSpotNotFoundException();

            var reservationId = new ReservationId(command.ReservationId);

            var reservation = weeklyParkingSpot
                .Reservations
                .OfType<VehicleReservation>()
                .SingleOrDefault(x => x.Id == reservationId);

            if (reservation is null)
                throw new ReservationNotFoundException(command.ReservationId);

            reservation.ChangeLicensePlate(command.LicensePlate);
            await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);
        }

        public async Task DeleteAsync(DeleteReservation command)
        {
            var weeklyReservation = await GetWeeklyParkingSpotReservation(command.ReservationId);

            if (weeklyReservation is null)
                throw new WeeklyParkingSpotNotFoundException();

            weeklyReservation.RemoveReservation(command.ReservationId);
            await _weeklyParkingSpotRepository.UpdateAsync(weeklyReservation);
        }

        private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotReservation(Guid id)
            => (await _weeklyParkingSpotRepository.GetAllAsync())
               .SingleOrDefault(x => x.Reservations.Any(y => y.Id == new ReservationId(id)));

        public async Task ReserveParkingForCleaningAsync(ReserveParkingSpotForCleaning command)
        {
            var week = new Week(command.Date);
            var weeklyParkingSpots =
                (await _weeklyParkingSpotRepository.GetByWeekAsync(week));

            var reservation = new CleaningReservation(ReservationId.Create(), new Date(command.Date));
            _parkingReservationService.ReserveSpotForCleaning(weeklyParkingSpots, new Date(command.Date));

            foreach (var parkingSpot in weeklyParkingSpots)
            {
                await _weeklyParkingSpotRepository.UpdateAsync(parkingSpot);
            }
        }
    }
}
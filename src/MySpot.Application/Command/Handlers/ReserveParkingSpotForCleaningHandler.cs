using MySpot.Application.Abstractions;
using MySpot.Core.DomainServices;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Command.Handlers
{
    public sealed class ReserveParkingSpotForCleaningHandler : ICommandHandler<ReserveParkingSpotForCleaning>
    {
        private readonly IWeeklyParkingSpotRepository _repository;
        private readonly IParkingReservationService _reservationService;

        public ReserveParkingSpotForCleaningHandler(IWeeklyParkingSpotRepository repository, 
            IParkingReservationService reservationService)
        {
            _repository = repository;
            _reservationService = reservationService;
        }

        public async Task HandleAsync(ReserveParkingSpotForCleaning command)
        {
            var week = new Week(command.Date);
            var weeklyParkingSpot = (await _repository.GetByWeekAsync(week));

            _reservationService.ReserveSpotForCleaning(weeklyParkingSpot, new Date(command.Date));

            var task = weeklyParkingSpot.Select(x => _repository.UpdateAsync(x));
            await Task.WhenAll(task);
        }
    }
}
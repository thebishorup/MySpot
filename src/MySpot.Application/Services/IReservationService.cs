using MySpot.Application.Command;
using MySpot.Application.DTO;

namespace MySpot.Application.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync();
        Task<ReservationDto> GetAsync(Guid id);
        Task ReserveSpotForVehicleAsync(ReserveParkingSpotForVehicle command);
        Task ReserveParkingForCleaningAsync(ReserveParkingSpotForCleaning command);
        Task ChangeReservedVehicleLicensePlateAsync(ChangeReservationLicensePlate command);
        Task DeleteAsync(DeleteReservation command);
    }
}
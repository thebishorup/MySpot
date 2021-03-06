using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.DomainServices;
public interface IParkingReservationService
{
    void ReserveSpotForVehicle(IEnumerable<WeeklyParkingSpot> allParkingSpots, JobTitle jobTitle,
        WeeklyParkingSpot parkingSpotToReserver, VehicleReservation reservation);
    void ReserveSpotForCleaning(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, Date date);
}
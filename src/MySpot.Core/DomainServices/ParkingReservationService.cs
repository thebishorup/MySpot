using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.Exceptions;
using MySpot.Core.Policies;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.DomainServices;

public sealed class ParkingReservationService : IParkingReservationService
{
    private readonly IEnumerable<IReservationPolicy> _reservationPolicies;
    private readonly IClock _clock;

    public ParkingReservationService(IEnumerable<IReservationPolicy> reservationPolicy, IClock clock)
    {
        _reservationPolicies = reservationPolicy;
        _clock = clock;
    }

    public void ReserveSpotForVehicle(IEnumerable<WeeklyParkingSpot> allParkingSpots,
        JobTitle jobTitle, WeeklyParkingSpot parkingSpotToReserver,
        VehicleReservation reservation)
    {
        var policy = _reservationPolicies
            .SingleOrDefault(x => x.CanBeApplied(jobTitle));

        if (policy is null)
            throw new NoReservationPolicyFoundException(jobTitle);

        if (!policy.CanReserve(allParkingSpots, reservation.EmployeeName))
            throw new CannotReserverParkingSpotReservationException(parkingSpotToReserver.Id);

        parkingSpotToReserver.AddReservation(reservation, new Date(_clock.Current()));
    }

    public void ReserveSpotForCleaning(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, Date date)
    {
        foreach (var parkingSpot in weeklyParkingSpots)
        {
            var reservationsForSameDate = parkingSpot.Reservations
                .Where(x => x.Date == date);

            parkingSpot.RemoveReservations(reservationsForSameDate);
            parkingSpot.AddReservation(new CleaningReservation(ReservationId.Create(), date), new Date(_clock.Current()));
        }
    }
}
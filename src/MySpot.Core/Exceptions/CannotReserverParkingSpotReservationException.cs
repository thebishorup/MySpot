using MySpot.Core.ValueObjects;

namespace MySpot.Core.Exceptions
{
    public sealed class CannotReserverParkingSpotReservationException : CustomException
    {
        public CannotReserverParkingSpotReservationException(ParkingSpotId parkingSpotId)
            : base($"Cannot reserve the parking spot with id {parkingSpotId}")
        {
            ParkingSpotId = parkingSpotId;
        }

        public ParkingSpotId ParkingSpotId { get; }
    }
}

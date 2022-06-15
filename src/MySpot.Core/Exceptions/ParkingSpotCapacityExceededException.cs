using MySpot.Core.ValueObjects;

namespace MySpot.Core.Exceptions
{
    public sealed class ParkingSpotCapacityExceededException : CustomException
    {
        public ParkingSpotId Value { get; }
        public ParkingSpotCapacityExceededException(ParkingSpotId parkingSpotId) 
            : base($"Parking spot with id {parkingSpotId} exceeded capacity.")
        {
            Value = parkingSpotId;
        }
    }
}

using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions
{
    public sealed class WeeklyParkingSpotNotFoundException : CustomException
    {
        public WeeklyParkingSpotNotFoundException(Guid id) 
            : base($"Weekly parking spot with id {id} not found.")
        {
            Id = id;
        }

        public WeeklyParkingSpotNotFoundException() : this(Guid.Empty)
        {

        }

        public Guid? Id { get; }
    }
}
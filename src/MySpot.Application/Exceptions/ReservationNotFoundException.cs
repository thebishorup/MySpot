using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions
{
    public sealed class ReservationNotFoundException : CustomException
    {
        public ReservationNotFoundException(Guid id)
            : base($"Reservation with id {id} not found.")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}

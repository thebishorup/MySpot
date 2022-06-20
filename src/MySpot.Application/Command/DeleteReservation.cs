using MySpot.Application.Abstractions;

namespace MySpot.Application.Command;
public sealed record DeleteReservation(Guid ReservationId) : ICommand;
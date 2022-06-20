using MySpot.Application.Abstractions;

namespace MySpot.Application.Command;

public sealed record ChangeReservationLicensePlate(Guid ReservationId, string LicensePlate) : ICommand;
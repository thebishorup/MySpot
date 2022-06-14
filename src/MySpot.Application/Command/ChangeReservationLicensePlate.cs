namespace MySpot.Application.Command;

public sealed record ChangeReservationLicensePlate(Guid ReservationId, string LicensePlate);
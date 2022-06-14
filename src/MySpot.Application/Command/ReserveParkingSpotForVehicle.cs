namespace MySpot.Application.Command;

public sealed record ReserveParkingSpotForVehicle(Guid ParkingSpotId, Guid ReservationId,
    string EmployeeName, string LicensePlate, DateTime Date);
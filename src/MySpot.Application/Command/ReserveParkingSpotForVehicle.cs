using MySpot.Application.Abstractions;

namespace MySpot.Application.Command;

public sealed record ReserveParkingSpotForVehicle(Guid ParkingSpotId, Guid ReservationId,
    string EmployeeName, string LicensePlate, int Capacity, DateTime Date) : ICommand;
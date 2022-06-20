using MySpot.Application.Abstractions;

namespace MySpot.Application.Command;

public sealed record ReserveParkingSpotForCleaning(DateTime Date) : ICommand;
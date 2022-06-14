namespace MySpot.Core.Exceptions
{
    public sealed class PakingSpotAlreadyReservedException : CustomException
    {
        public DateTime Date { get; }
        public string ParkingSpotName { get; }
        public PakingSpotAlreadyReservedException(DateTime date, string name)
            : base($"Parking spot {name} is already reserved for the date {date}.")
        {
            Date = date;
            ParkingSpotName = name;
        }
    }
}

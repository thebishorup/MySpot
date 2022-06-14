namespace MySpot.Core.Exceptions
{
    public sealed class InvalidParkingSpotNameException : CustomException
    {
        public InvalidParkingSpotNameException()
            : base("Invalid parking spot name.")
        {
        }
    }
}

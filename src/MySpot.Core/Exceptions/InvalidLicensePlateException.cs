namespace MySpot.Core.Exceptions;
public sealed class InvalidLicensePlateException : CustomException
{
    public string License { get; }
    public InvalidLicensePlateException(string licensePlate)
        : base($"License plate {licensePlate} is invalid.")
    {
        License = licensePlate;
    }
}

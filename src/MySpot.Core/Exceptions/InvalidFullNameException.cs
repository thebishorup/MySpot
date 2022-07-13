namespace MySpot.Core.Exceptions
{
    public sealed class InvalidFullNameException : CustomException
    {
        public string Value { get; }
        public InvalidFullNameException(string fullName)
            : base($"Invalid full name {fullName}. Username length should be greater than 3 and less than 100")
        {
            Value = fullName;
        }
    }
}

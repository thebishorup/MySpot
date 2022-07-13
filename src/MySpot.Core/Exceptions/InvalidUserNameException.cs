namespace MySpot.Core.Exceptions
{
    public sealed class InvalidUserNameException : CustomException
    {
        public string Value { get; }
        public InvalidUserNameException(string userName)
            : base($"Invalid user name {userName}. Username length should be greater than 3 and less than 30")
        {
            Value = userName;
        }
    }
}

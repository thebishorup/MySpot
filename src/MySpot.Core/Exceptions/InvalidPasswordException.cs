namespace MySpot.Core.Exceptions
{
    public sealed class InvalidPasswordException : CustomException
    {
        public string Value { get; }
        public InvalidPasswordException(string password)
            : base($"Invalid password {password}.")
        {
            Value = password;
        }
    }
}

using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions
{
    public sealed class UserNameAlreadyInUserException : CustomException
    {
        public UserNameAlreadyInUserException(string userName)
            : base($"Provided user name {userName} already in use.")
        {
            Value = userName;
        }

        public string Value { get; }
    }
}

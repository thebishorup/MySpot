using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions
{
    public sealed class EmailAlreadyInUserException : CustomException
    {
        public EmailAlreadyInUserException(string email)
            : base($"Provided email {email} already in use.")
        {
            Value = email;
        }

        public string Value { get; }
    }
}

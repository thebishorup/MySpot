using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions
{
    public sealed class UserNotFoundException : CustomException
    {
        public UserNotFoundException(Guid id)
            : base($"User with id {id} not found.")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}

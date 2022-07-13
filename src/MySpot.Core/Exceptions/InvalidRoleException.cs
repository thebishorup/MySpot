namespace MySpot.Core.Exceptions
{
    public sealed class InvalidRoleException : CustomException
    {
        public string Value { get; }
        public InvalidRoleException(string role)
            : base($"Invalid role {role}.")
        {
            Value = role;
        }
    }
}

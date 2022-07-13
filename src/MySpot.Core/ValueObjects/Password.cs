using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects
{
    public sealed record Password
    {
        public static IEnumerable<string> AvailableRoles { get; } = new[] { "admin", "user" };
        public string Value { get; }

        public Password(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length is > 200 or < 6)
                throw new InvalidFullNameException(value);

            Value = value;
        }

        public static implicit operator string(Password value) => value.Value;
        public static implicit operator Password(string value) => new(value);
        public override string ToString() => Value;
    }
}

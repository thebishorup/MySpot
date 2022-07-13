using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects
{
    public sealed record UserName
    {
        public string Value { get; }

        public UserName(string value)
        {
            if(string.IsNullOrEmpty(value) || value.Length is > 30 or < 3)
                throw new InvalidUserNameException(value);

            Value = value;
        }

        public static implicit operator string(UserName value) => value.Value;
        public static implicit operator UserName(string value) => new(value);
        public override string ToString() => Value;
    }
}

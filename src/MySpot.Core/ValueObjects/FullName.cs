using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects
{
    public sealed record FullName
    {
        public string Value { get; }

        public FullName(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length is > 100 or < 3)
                throw new InvalidFullNameException(value);

            Value = value;
        }

        public static implicit operator string(FullName value) => value.Value;
        public static implicit operator FullName(string value) => new(value);
        public override string ToString() => Value;
    }
}

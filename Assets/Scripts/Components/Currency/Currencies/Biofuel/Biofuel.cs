using System;

namespace ADC.Currencies
{
    public struct Biofuel : ICurrency
    {
        public decimal Value { get; private set; }

        public Biofuel(decimal amount)
        {
            Value = amount;
        }

        //public static implicit operator Biofuel(decimal value) => new(value);
        public static implicit operator decimal(Biofuel biofuel) => biofuel.Value;
        public static bool operator ==(Biofuel x, Biofuel y) => x.Value == y.Value;
        public static bool operator !=(Biofuel x, Biofuel y) => x.Value != y.Value;
        public static bool operator ==(Biofuel x, decimal y) => x.Value == y;
        public static bool operator !=(Biofuel x, decimal y) => x.Value != y;
        public static bool operator ==(decimal y, Biofuel x) => x.Value == y;
        public static bool operator !=(decimal y, Biofuel x) => x.Value != y;
        public static bool operator >(Biofuel x, Biofuel y) => x.Value > y.Value;
        public static bool operator <(Biofuel x, Biofuel y) => x.Value < y.Value;
        public static bool operator >=(Biofuel x, Biofuel y) => x.Value > y.Value;
        public static bool operator <=(Biofuel x, Biofuel y) => x.Value < y.Value;
        public static bool operator >(Biofuel x, decimal y) => x.Value > y;
        public static bool operator <(Biofuel x, decimal y) => x.Value < y;
        public static bool operator >=(Biofuel x, decimal y) => x.Value > y;
        public static bool operator <=(Biofuel x, decimal y) => x.Value < y;
        public static bool operator >(decimal x, Biofuel y) => x > y.Value;
        public static bool operator <(decimal x, Biofuel y) => x < y.Value;
        public static bool operator >=(decimal x, Biofuel y) => x > y.Value;
        public static bool operator <=(decimal x, Biofuel y) => x < y.Value;

        public static Biofuel operator +(Biofuel x, Biofuel y)
        {
            return new Biofuel(y.Value + x.Value);
        }

        public static Biofuel operator -(Biofuel x, Biofuel y)
        {
            return new Biofuel(Math.Max(0, x.Value - y.Value));
        }

        public override string ToString()
        {
            return $"{Value}";
        }

        public bool IsEmpty => Value == default;
    }
}
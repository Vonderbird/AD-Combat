using System;
using ADC.API;

namespace ADC.Currencies
{

    public struct WarScrap : ICurrency
    {
        public decimal Value { get; private set; }

        public WarScrap(decimal amount)
        {
            Value = amount;
        }

        //public static implicit operator WarScrap(decimal value) => new(value);
        public static implicit operator decimal(WarScrap biofuel) => biofuel.Value;
        public static bool operator ==(WarScrap x, WarScrap y) => x.Value == y.Value;
        public static bool operator !=(WarScrap x, WarScrap y) => x.Value != y.Value;
        public static bool operator ==(WarScrap x, decimal y) => x.Value == y;
        public static bool operator !=(WarScrap x, decimal y) => x.Value != y;
        public static bool operator ==(decimal y, WarScrap x) => x.Value == y;
        public static bool operator !=(decimal y, WarScrap x) => x.Value != y;
        public static bool operator >(WarScrap x, WarScrap y) => x.Value > y.Value;
        public static bool operator <(WarScrap x, WarScrap y) => x.Value < y.Value;
        public static bool operator >=(WarScrap x, WarScrap y) => x.Value > y.Value;
        public static bool operator <=(WarScrap x, WarScrap y) => x.Value < y.Value;
        public static bool operator >(WarScrap x, decimal y) => x.Value > y;
        public static bool operator <(WarScrap x, decimal y) => x.Value < y;
        public static bool operator >=(WarScrap x, decimal y) => x.Value > y;
        public static bool operator <=(WarScrap x, decimal y) => x.Value < y;
        public static bool operator >(decimal x, WarScrap y) => x > y.Value;
        public static bool operator <(decimal x, WarScrap y) => x < y.Value;
        public static bool operator >=(decimal x, WarScrap y) => x > y.Value;
        public static bool operator <=(decimal x, WarScrap y) => x < y.Value;


        public static WarScrap operator +(WarScrap x, decimal y) => new(x.Value + y);
        public static WarScrap operator -(WarScrap x, decimal y) => new(Math.Max(0, x.Value - y));
        public static WarScrap operator /(WarScrap x, decimal y) => new(x.Value / y);
        public static WarScrap operator *(WarScrap x, decimal y) => new(x.Value * y);
        public static WarScrap operator +(WarScrap x, WarScrap y) => new(y.Value + x.Value);
        public static WarScrap operator -(WarScrap x, WarScrap y) => new(Math.Max(0, x.Value - y.Value));


        public override string ToString()
        {
            return $"{Value}";
        }

        public bool IsEmpty => Value == default;


        public bool Equals(WarScrap warScrap)
        {
            return this.Value == warScrap.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !obj.IsNumber()) return false;
            return this.Equals(new WarScrap(decimal.Parse(obj.ToString())));
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
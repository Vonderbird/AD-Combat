using System;


public struct WarScrap : ICurrency
{
    public decimal Value { get; private set; }

    public WarScrap(decimal amount)
    {
        Value = amount;
    }

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

    public static WarScrap operator +(WarScrap x, WarScrap y)
    {
        return new WarScrap(y.Value + x.Value);
    }
    public static WarScrap operator -(WarScrap x, WarScrap y)
    {
        return new WarScrap(Math.Max(0, x.Value - y.Value));
    }

    public bool IsEmpty => Value == default;
}
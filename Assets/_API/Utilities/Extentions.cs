namespace ADC.API
{
    public static class Extentions
    {
        public static bool IsNumber(this object value)
        {
            return value is sbyte
                   || value is byte
                   || value is short
                   || value is ushort
                   || value is int
                   || value is uint
                   || value is long
                   || value is ulong
                   || value is float
                   || value is double
                   || value is decimal;
        }
        
        public static CurrencyChangeEventArgs ToNonGeneric<T>(this CurrencyChangeEventArgs<T> b) where T:struct, ICurrency
        {
            return new CurrencyChangeEventArgs(b.FactionId, b.ChangeType, b.Difference, b.NewValue);
        }
    }
}
namespace ADC.Currencies
{

    public interface ICurrency
    {
        public decimal Value { get; }
        public bool IsEmpty { get; }
    }
}
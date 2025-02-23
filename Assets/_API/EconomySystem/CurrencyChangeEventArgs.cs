namespace ADC.API
{
    public struct CurrencyChangeEventArgs 
    {
        public CurrencyChangeEventArgs(int factionId, CurrencyChangeType changeType, ICurrency difference, ICurrency newValue)
        {
            FactionId = factionId;
            ChangeType = changeType;
            Difference = difference;
            NewValue = newValue;
        }

        public int FactionId { get; }
        public CurrencyChangeType ChangeType { get; }
        public ICurrency Difference { get; }
        public ICurrency NewValue { get; }
    }
    
    public struct CurrencyChangeEventArgs<T> where T : struct, ICurrency
    {
        public CurrencyChangeEventArgs(int factionId, T difference, T newValue, CurrencyChangeType changeType)
        {
            Difference = difference;
            NewValue = newValue;
            ChangeType = changeType;
            FactionId = factionId;
        }

        public int FactionId { get; private set; }
        public CurrencyChangeType ChangeType { get; private set; }
        public T Difference { get; private set; }
        public T NewValue { get; private set; }

    }
    
    public enum CurrencyChangeType
    {
        Init,
        Withdraw,
        Deposit
    }
}
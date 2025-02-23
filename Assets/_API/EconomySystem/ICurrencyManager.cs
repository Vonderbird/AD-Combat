using System;
using System.Collections.Generic;


namespace ADC.API
{
    public interface ICurrencyManager<T> where T : struct, ICurrency
    {
        T SaveAmount { get; }
        int FactionId { get; }
        void Init(T initAmount);
        bool Deposit(T amount);
        bool Withdraw(T amount);
        event EventHandler<CurrencyChangeEventArgs<T>> ValueChanged;
    }

    public enum CurrencyChangeType
    {
        Init,
        Withdraw,
        Deposit
    }

    public interface ICurrencyChangeEventArgs<out T> where T : struct, ICurrency
    {
        int FactionId { get; }
        CurrencyChangeType ChangeType { get; }
        T Difference { get; }
        T NewValue { get; }
    }

    public struct CurrencyChangeEventArgs<T> : ICurrencyChangeEventArgs<T> where T : struct, ICurrency
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
}
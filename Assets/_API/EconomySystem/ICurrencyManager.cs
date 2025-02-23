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

}
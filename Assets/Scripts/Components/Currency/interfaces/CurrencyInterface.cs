using ADC.API;
using UnityEngine;
using Zenject;

namespace ADC.Currencies
{
    public abstract class CurrencyInterface<T> : CurrencyInterface where T : struct, ICurrency
    {
        public abstract void Refresh(CurrencyChangeEventArgs<T> args);
    }
}
using UnityEngine;

public abstract class CurrencyVisualizer: MonoBehaviour
{
    public abstract void Refresh<T>(CurrencyChangeEventArgs<T> args) where T : struct, ICurrency;
}
using UnityEngine.Events;

public abstract class CurrencyManager<T> where T : struct, ICurrency
{
    protected T saveAmount;

    public T SaveAmount => saveAmount;

    public abstract bool Deposit(T amount);

    public abstract bool Withdraw(T amount);

    public UnityEvent<CurrencyChangeEventArgs<T>> ValueChanged;
}

public enum CurrencyChangeType
{
    WITHDRAW,
    DEPOSIT
}

public struct CurrencyChangeEventArgs<T> where T : struct, ICurrency
{
    public CurrencyChangeEventArgs(T difference, T newValue, CurrencyChangeType changeType)
    {
        Difference = difference;
        NewValue = newValue;
        ChangeType = changeType;
    }
    public CurrencyChangeType ChangeType { get; set; }
    public T Difference { get; set; }
    public T NewValue { get; set; }
}

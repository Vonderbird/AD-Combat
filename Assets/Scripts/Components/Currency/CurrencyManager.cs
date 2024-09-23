using UnityEngine.Events;

public abstract class CurrencyManager<T> where T : struct, ICurrency
{
    protected int factionId;
    protected T saveAmount;

    public T SaveAmount => saveAmount;
    protected int FactionId => factionId;

    public abstract bool Deposit(T amount);

    public abstract bool Withdraw(T amount);

    public UnityEvent<CurrencyChangeEventArgs<T>> ValueChanged = new();
}

public enum CurrencyChangeType
{
    WITHDRAW,
    DEPOSIT
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

public class BiofuelManager : CurrencyManager<Biofuel>
{
    public BiofuelManager(decimal initialAmount = 0)
    {
        saveAmount = new Biofuel(initialAmount);
    }
    public override bool Deposit(Biofuel amount)
    {
        if (amount.IsEmpty) return false;
        saveAmount += amount;
        ValueChanged?.Invoke(new CurrencyChangeEventArgs<Biofuel>(amount, saveAmount, CurrencyChangeType.DEPOSIT));
        return true;
    }

    public override bool Withdraw(Biofuel amount)
    {
        if (amount.IsEmpty) return false;
        if (amount > saveAmount) return false;
        saveAmount -= amount;
        ValueChanged?.Invoke(new CurrencyChangeEventArgs<Biofuel>(amount, saveAmount, CurrencyChangeType.WITHDRAW));
        return true;
    }
}
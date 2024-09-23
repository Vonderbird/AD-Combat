public class BiofuelManager : CurrencyManager<Biofuel>
{
    public BiofuelManager(int factionId, decimal initialAmount = 0)
    {
        this.factionId = factionId;
        saveAmount = new Biofuel(initialAmount);
    }

    public override bool Deposit(Biofuel amount)
    {
        if (amount.IsEmpty) return false;
        saveAmount += amount;
        ValueChanged?.Invoke(new CurrencyChangeEventArgs<Biofuel>(factionId, amount, saveAmount, CurrencyChangeType.DEPOSIT));
        return true;
    }

    public override bool Withdraw(Biofuel amount)
    {
        if (amount.IsEmpty) return false;
        if (amount > saveAmount) return false;
        saveAmount -= amount;
        ValueChanged?.Invoke(new CurrencyChangeEventArgs<Biofuel>(factionId, amount, saveAmount, CurrencyChangeType.WITHDRAW));
        return true;
    }
}
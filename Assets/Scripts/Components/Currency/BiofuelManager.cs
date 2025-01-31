using ADC.API;

namespace ADC.Currencies
{
    public class BiofuelManager : CurrencyManager<Biofuel>
    {
        public BiofuelManager(int factionId)
        {
            this.factionId = factionId;
        }

        public override void Init(Biofuel saveAmount)
        {
            if (isInitialized) return;
            this.saveAmount = saveAmount;
            ValueChanged?.Invoke(new CurrencyChangeEventArgs<Biofuel>(factionId, new Biofuel(0m), saveAmount, CurrencyChangeType.INIT));
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

}
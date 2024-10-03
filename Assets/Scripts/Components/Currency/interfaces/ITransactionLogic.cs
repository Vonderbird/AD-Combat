namespace ADC.Currencies
{

    public interface ITransactionArgs
    {
    }

    public abstract class TransactionLogic<T> : ICurrencyDrainer, ICurrencyProducer
        where T : ITransactionArgs
    {
        protected readonly int factionId;

        protected TransactionLogic(int factionId)
        {
            this.factionId = factionId;
        }

        public abstract bool Pay(T args);

        public bool Drain(Biofuel amount)
        {
            return EconomySystem.Instance[factionId].Withdraw(amount);
        }

        public bool Drain(WarScrap amount)
        {
            return EconomySystem.Instance[factionId].Withdraw(amount);
        }

        public bool Produce(Biofuel amount)
        {
            return EconomySystem.Instance[factionId].Deposit(amount);
        }

        public bool Produce(WarScrap amount)
        {
            return EconomySystem.Instance[factionId].Deposit(amount);
        }
    }
}
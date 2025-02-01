using ADC.API;
using Zenject;

namespace ADC.Currencies
{

    public interface ITransactionArgs
    {
    }

    public abstract class TransactionLogic<T> : ICurrencyDrainer, ICurrencyProducer
        where T : ITransactionArgs
    {
        private readonly IEconomySystem economySystem;
        protected readonly int factionId;

        [Inject]
        protected TransactionLogic(IEconomySystem economySystem, int factionId)
        {
            this.economySystem = economySystem;
            this.factionId = factionId;
        }

        public abstract bool Process(T args);

        public bool Drain(Biofuel amount)
        {
            return economySystem[factionId].Withdraw(amount);
        }

        public bool Drain(WarScrap amount)
        {
            return economySystem[factionId].Withdraw(amount);
        }

        public bool Produce(Biofuel amount)
        {
            return economySystem[factionId].Deposit(amount);
        }

        public bool Produce(WarScrap amount)
        {
            return economySystem[factionId].Deposit(amount);
        }
    }
}
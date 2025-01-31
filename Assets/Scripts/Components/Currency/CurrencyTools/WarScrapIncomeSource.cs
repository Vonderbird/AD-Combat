using ADC.API;
using Zenject;

namespace ADC.Currencies
{
    public class WarScrapIncomeSource : IncomeSource
    {
        private readonly WarScrap paymentAmount;
        public override decimal PaymentAmount => paymentAmount.Value;
        private IEconomySystem economySystem;

        [Inject]
        private void Construct(IEconomySystem economySystem)
        {
            this.economySystem = economySystem;
        }

        public WarScrapIncomeSource(WarScrap paymentAmount, int factionId): 
            base(factionId)
        {
            this.paymentAmount = paymentAmount;
        }

        protected override void Update()
        {

            //Debug.Log($"Update with income value: {PaymentAmount}");
            economySystem[factionId].Deposit(paymentAmount);
        }
    }
}
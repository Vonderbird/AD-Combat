using ADC.API;
using Zenject;

namespace ADC.Currencies
{
    public class WarScrapIncomeSource : IncomeSource
    {
        private readonly WarScrap paymentAmount;
        public override decimal PaymentAmount => paymentAmount.Value;


        [Inject]
        public WarScrapIncomeSource(IWaveTimer waveTimer, IEconomySystem economySystem, WarScrap paymentAmount, int factionId): 
            base(waveTimer, economySystem, factionId)
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
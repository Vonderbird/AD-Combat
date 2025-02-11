using ADC.API;

namespace ADC.Currencies
{
    public class WarScrapIncomeSource : IncomeSource
    {
        private readonly WarScrap paymentAmount;
        public override ICurrency PaymentAmount => paymentAmount;


        public WarScrapIncomeSource(IWaveTimer waveTimer, WarScrap paymentAmount, int factionId): 
            base(waveTimer, factionId)
        {
            this.paymentAmount = paymentAmount;
        }
        //
        // protected override void Update()
        // {
        //
        //     //Debug.Log($"Update with income value: {PaymentAmount}");
        //     EconomySystem[FactionId].Deposit(paymentAmount);
        // }
    }
}
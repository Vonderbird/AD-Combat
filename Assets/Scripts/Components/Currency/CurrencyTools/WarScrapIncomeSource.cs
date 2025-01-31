using UnityEngine;

namespace ADC.Currencies
{
    public class WarScrapIncomeSource : IncomeSource
    {
        private readonly WarScrap paymentAmount;
        public override decimal PaymentAmount => paymentAmount.Value;
        public WarScrapIncomeSource(WarScrap paymentAmount, int factionId): 
            base(factionId)
        {
            this.paymentAmount = paymentAmount;
        }


        protected override void Update()
        {

            //Debug.Log($"Update with income value: {PaymentAmount}");
            EconomySystem.Instance[factionId].Deposit(paymentAmount);
        }
    }
}
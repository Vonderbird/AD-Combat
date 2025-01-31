using ADC.API;
using UnityEngine;

namespace ADC.Currencies
{
    public class BiofuelIncomeSource : IncomeSource
    {
        private readonly Biofuel paymentAmount;
        public override decimal PaymentAmount => paymentAmount.Value;
        public BiofuelIncomeSource(Biofuel paymentAmount, int factionId) :
            base(factionId)
        {
            this.paymentAmount = paymentAmount;
        }



        protected override void Update()
        {
            Debug.Log($"Update with income value: {PaymentAmount}");
            EconomySystem.Instance[factionId].Deposit(paymentAmount);
        }
    }
}
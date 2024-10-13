using UnityEngine;

namespace ADC.Currencies
{
    public class BioFuelIncomeSource : IncomeSource
    {
        private readonly Biofuel paymentAmount;
        public override decimal PaymentAmount => paymentAmount.Value;
        public BioFuelIncomeSource(Biofuel paymentAmount, int factionId) :
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
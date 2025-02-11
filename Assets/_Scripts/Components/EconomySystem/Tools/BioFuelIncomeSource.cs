using ADC.API;
using UnityEngine;

namespace ADC.Currencies
{
    
    public class BiofuelIncomeSource : IncomeSource
    {
        private readonly Biofuel paymentAmount;
        public override ICurrency PaymentAmount => paymentAmount;

        public BiofuelIncomeSource(IWaveTimer waveTimer, Biofuel paymentAmount, int factionId) :
            base(waveTimer, factionId)
        {
            this.paymentAmount = paymentAmount;
        }

        // protected override void Update()
        // {
        //     Debug.Log($"Update with income value: {PaymentAmount}");
        //     EconomySystem[FactionId].Deposit(paymentAmount);
        // }
    }
}
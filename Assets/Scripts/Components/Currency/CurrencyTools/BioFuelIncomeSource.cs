using ADC.API;
using UnityEngine;
using Zenject;

namespace ADC.Currencies
{
    
    public class BiofuelIncomeSource : IncomeSource
    {
        private readonly Biofuel paymentAmount;
        public override decimal PaymentAmount => paymentAmount.Value;

        [Inject]
        public BiofuelIncomeSource(IWaveTimer waveTimer, IEconomySystem economySystem, Biofuel paymentAmount, int factionId) :
            base(waveTimer, economySystem, factionId)
        {
            this.paymentAmount = paymentAmount;
        }

        protected override void Update()
        {
            Debug.Log($"Update with income value: {PaymentAmount}");
            economySystem[factionId].Deposit(paymentAmount);
        }
    }
}
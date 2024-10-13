using System;
using UnityEngine;

namespace ADC.Currencies
{
    public abstract class IncomeSource :IDisposable
    {
        public Guid IncomeId { get; }
        private readonly Coroutine updater;
        private readonly WaitUntil wait;
        protected readonly int factionId;
        public abstract decimal PaymentAmount { get; }

        protected IncomeSource(int factionId)
        {
            IncomeId = Guid.NewGuid();
            this.factionId = factionId;
            EconomySystem.Instance.StartWave.AddListener(Update);
        }
        
        public void Dispose()
        {
            EconomySystem.Instance.StartWave.RemoveListener(Update);
        }

        protected abstract void Update();
    }
}
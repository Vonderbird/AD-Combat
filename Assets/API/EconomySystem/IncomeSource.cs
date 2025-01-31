using System;
using UnityEngine;
using Zenject;

namespace ADC.API
{
    public abstract class IncomeSource :IDisposable
    {
        public Guid IncomeId { get; }
        private readonly Coroutine updater;
        private readonly WaitUntil wait;
        protected readonly int factionId;
        public abstract decimal PaymentAmount { get; }
        protected IWaveTimer waveTimer;
        protected IEconomySystem economySystem;

        protected IncomeSource(IWaveTimer waveTimer, IEconomySystem economySystem, int factionId)
        {
            this.waveTimer = waveTimer;
            IncomeId = Guid.NewGuid();
            this.factionId = factionId;
            waveTimer.Begin.AddListener(Update);
            this.economySystem = economySystem;
        }
        
        public void Dispose()
        {
            waveTimer.Begin.RemoveListener(Update);
        }

        protected abstract void Update();
    }
}
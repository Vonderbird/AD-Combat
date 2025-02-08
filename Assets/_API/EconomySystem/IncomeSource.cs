using System;
using Sisus.Init;
using UnityEngine;

namespace ADC.API
{
    public abstract class IncomeSource :IDisposable
    {
        public Guid IncomeId { get; private set; }
        private readonly Coroutine _updater;
        private readonly WaitUntil _wait;
        protected int FactionId;
        public abstract decimal PaymentAmount { get; }
        private IWaveTimer _waveTimer;
        protected IEconomySystem EconomySystem;

        public IncomeSource(IWaveTimer waveTimer, IEconomySystem economySystem, int factionId)
        {
            this._waveTimer = waveTimer;
            this.EconomySystem = economySystem;
            this.FactionId = factionId;
            IncomeId = Guid.NewGuid();
            waveTimer.Begin.AddListener(Update);
        }
        
        public void Dispose()
        {
            _waveTimer.Begin.RemoveListener(Update);
        }

        protected abstract void Update();
    }
}
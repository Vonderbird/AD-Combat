using System;
using Sisus.Init;
using UnityEngine;

namespace ADC.API
{
    public struct IncomeEventArgs
    {
        public ICurrency IncomeAmount { get; set; }
    }

    public interface IIncomeSource : IDisposable
    {
        Guid IncomeId { get; }
        ICurrency PaymentAmount { get; }
        event EventHandler<IncomeEventArgs> IncomeReceived;
    }

    public abstract class IncomeSource : IIncomeSource
    {
        public Guid IncomeId { get; private set; }
        private readonly Coroutine _updater;
        private readonly WaitUntil _wait;
        protected int FactionId;
        public abstract ICurrency PaymentAmount { get; }
        private IWaveTimer _waveTimer;

        public event EventHandler<IncomeEventArgs> IncomeReceived;

        public IncomeSource(IWaveTimer waveTimer, int factionId)
        {
            this._waveTimer = waveTimer;
            this.FactionId = factionId;
            IncomeId = Guid.NewGuid();
            waveTimer.Begin.AddListener(Update);
        }
        
        public void Dispose()
        {
            _waveTimer.Begin.RemoveListener(Update);
        }

        protected virtual void Update()
        {
            IncomeReceived?.Invoke(this, new IncomeEventArgs{IncomeAmount = PaymentAmount});
        }
    }
}
using System;
using System.Collections;
using RTSEngine.Determinism;
using UnityEngine;

namespace ADC.Currencies
{
    public abstract class IncomeSource :IDisposable
    {
        public Guid IncomeId { get; }
        private readonly float paymentPeriod;
        private readonly TimeModifiedTimer unitsTimer;
        private readonly Coroutine updater;
        private readonly WaitUntil wait;
        protected readonly int factionId;

        protected IncomeSource(float paymentPeriod, int factionId)
        {
            IncomeId = Guid.NewGuid();
            this.factionId = factionId;
            this.paymentPeriod = paymentPeriod;
            unitsTimer = new TimeModifiedTimer(paymentPeriod);
            wait = new WaitUntil(() => unitsTimer.ModifiedDecrease());
            updater = EconomySystem.Instance.StartCoroutine(EnumeratorUpdate());
        }
        
        public void Dispose()
        {
            if (updater != null)
                EconomySystem.Instance.StopCoroutine(updater);
        }

        public IEnumerator EnumeratorUpdate()
        {
            while (true)
            {
                Debug.Log($"paymentPeriod: {paymentPeriod}");
                yield return wait;
                Update();
                unitsTimer.Reload(paymentPeriod);
            }
        }

        protected abstract void Update();
    }
}
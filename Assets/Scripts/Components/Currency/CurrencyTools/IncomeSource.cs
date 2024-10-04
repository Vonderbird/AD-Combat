using System;
using System.Collections;
using RTSEngine.Determinism;
using UnityEngine;

namespace ADC.Currencies
{
    public abstract class IncomeSource :IDisposable
    {
        public Guid IncomeId { get; }
        private readonly TimeModifiedTimer unitsTimer;
        private readonly Coroutine updater;
        private readonly WaitUntil wait;
        protected readonly int factionId;
        public abstract decimal PaymentAmount { get; }
        public float PaymentPeriod { get; }

        protected IncomeSource(float paymentPeriod, int factionId)
        {
            IncomeId = Guid.NewGuid();
            this.factionId = factionId;
            this.PaymentPeriod = paymentPeriod;
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
                Debug.Log($"paymentPeriod: {PaymentPeriod}");
                yield return wait;
                Update();
                unitsTimer.Reload(PaymentPeriod);
            }
        }

        protected abstract void Update();
    }
}
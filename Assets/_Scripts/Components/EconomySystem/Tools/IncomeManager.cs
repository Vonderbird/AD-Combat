
using System;
using ADC.API;
using Zenject;
using UnityEngine.Events;
using System.Collections.Generic;

namespace ADC.Currencies
{
    public class IncomeManager: IIncomeManager
    {
        private readonly Dictionary<Guid, IncomeSource> incomeSources = new ();
        private readonly int factionId;
        public UnityEvent<decimal> IncomeChanged { get; }= new();
        private decimal totalIncomeRate = 0.0m;
        private IWaveTimer waveTimer;
        private IEconomySystem economySystem;
        private IIncomeSourceFactory incomeSourceFactory;
        [Inject]
        public IncomeManager(IIncomeSourceFactory incomeSourceFactory, int factionId)
        {
            this.factionId = factionId;
            this.incomeSourceFactory = incomeSourceFactory;
        }

        public Guid AddSource<T>(T amount) where T:ICurrency
        {
            var incomeSource = incomeSourceFactory.Create(amount, factionId);
            incomeSources.Add(incomeSource.IncomeId, incomeSource);
            totalIncomeRate += amount.Value;
            IncomeChanged?.Invoke(totalIncomeRate);
            return incomeSource.IncomeId;
        }

        public bool RemoveSource(Guid incomeId)
        {
            if (!incomeSources.TryGetValue(incomeId, out var source)) return false;
            source.Dispose();
            incomeSources.Remove(incomeId);
            totalIncomeRate -= source.PaymentAmount;
            IncomeChanged?.Invoke(totalIncomeRate);
            return true;
        }
    }
}

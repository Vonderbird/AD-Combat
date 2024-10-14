
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ADC.Currencies
{
    public class IncomeManager
    {
        private readonly Dictionary<Guid, IncomeSource> incomeSources = new ();
        private readonly int factionId;
        public readonly UnityEvent<decimal> IncomeChanged = new();
        private decimal totalIncomeRate = 0.0m;
        public IncomeManager(int factionId)
        {
            this.factionId = factionId;
        }
        public Guid AddSource(Biofuel amount)
        {
            var incomeSource = new BiofuelIncomeSource(amount, factionId);
            incomeSources.Add(incomeSource.IncomeId, incomeSource);
            totalIncomeRate += amount.Value;
            IncomeChanged?.Invoke(totalIncomeRate);
            return incomeSource.IncomeId;
        }
        public Guid AddSource(WarScrap amount)
        {
            var incomeSource = new WarScrapIncomeSource(amount, factionId);
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

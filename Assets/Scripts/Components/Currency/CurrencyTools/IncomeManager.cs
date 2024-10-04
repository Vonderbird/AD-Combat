
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
        public Guid AddSource(Biofuel amount, float paymentDuration)
        {
            var incomeSource = new BioFuelIncomeSource(amount, paymentDuration, factionId);
            incomeSources.Add(incomeSource.IncomeId, incomeSource);
            totalIncomeRate += (amount.Value / (decimal)paymentDuration);
            IncomeChanged?.Invoke(totalIncomeRate);
            return incomeSource.IncomeId;
        }
        public Guid AddSource(WarScrap amount, float paymentDuration)
        {
            var incomeSource = new WarScrapIncomeSource(amount, paymentDuration, factionId);
            incomeSources.Add(incomeSource.IncomeId, incomeSource);
            totalIncomeRate += (amount.Value / (decimal)paymentDuration);
            IncomeChanged?.Invoke(totalIncomeRate);
            return incomeSource.IncomeId;
        }

        public bool RemoveSource(Guid incomeId)
        {
            if (!incomeSources.TryGetValue(incomeId, out var source)) return false;
            source.Dispose();
            incomeSources.Remove(incomeId);
            totalIncomeRate -= (source.PaymentAmount / (decimal)source.PaymentPeriod);
            IncomeChanged?.Invoke(totalIncomeRate);
            return true;
        }
    }
}

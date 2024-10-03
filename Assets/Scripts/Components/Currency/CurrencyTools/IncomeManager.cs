
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ADC.Currencies
{
    public class IncomeManager
    {
        private readonly Dictionary<Guid, IncomeSource> incomeSources = new ();
        private readonly int factionId;
        public IncomeManager(int factionId)
        {
            this.factionId = factionId;
        }
        public Guid AddSource(Biofuel amount, float paymentDuration)
        {
            var incomeSource = new BioFuelIncomeSource(amount, paymentDuration, factionId);
            incomeSources.Add(incomeSource.IncomeId, incomeSource);
            return incomeSource.IncomeId;
        }
        public Guid AddSource(WarScrap amount, float paymentDuration)
        {
            var incomeSource = new WarScrapIncomeSource(amount, paymentDuration, factionId);
            incomeSources.Add(incomeSource.IncomeId, incomeSource);
            return incomeSource.IncomeId;
        }

        public bool RemoveSource(Guid incomeId)
        {
            if (!incomeSources.ContainsKey(incomeId)) return false;
            Debug.Log($"Remove source: {incomeId}");
            incomeSources[incomeId].Dispose();
            incomeSources.Remove(incomeId);
            return true;
        }
    }
}

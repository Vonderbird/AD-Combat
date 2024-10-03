
using System;
using System.Collections.Generic;

namespace ADC.Currencies
{
    public class IncomeManager
    {
        private Dictionary<Guid, IncomeSource> IncomeSources = new ();
        private readonly int factionId;
        public IncomeManager(int factionId)
        {
            this.factionId = factionId;
        }
        public Guid AddSource(Biofuel amount, float paymentDuration)
        {
            var incomeSource = new BioFuelIncomeSource(amount, paymentDuration, factionId);
            IncomeSources.Add(incomeSource.IncomeId, incomeSource);
            return incomeSource.IncomeId;
        }
        public Guid AddSource(WarScrap amount, float paymentDuration)
        {
            var incomeSource = new WarScrapIncomeSource(amount, paymentDuration, factionId);
            IncomeSources.Add(incomeSource.IncomeId, incomeSource);
            return incomeSource.IncomeId;
        }

        public bool RemoveSource(Guid incomeId)
        {
            if (IncomeSources.ContainsKey(incomeId))
            {
                IncomeSources.Remove(incomeId);
                return true;
            }
            return false;
        }
    }
}

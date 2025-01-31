
using System;
using System.Collections.Generic;
using ADC.API;
using UnityEngine;
using UnityEngine.Events;

namespace ADC.Currencies
{
    public class IncomeManager: IIncomeManager
    {
        private readonly Dictionary<Guid, IncomeSource> incomeSources = new ();
        private readonly int factionId;
        public UnityEvent<decimal> IncomeChanged { get; }= new();
        private decimal totalIncomeRate = 0.0m;
        public IncomeManager(int factionId)
        {
            this.factionId = factionId;
        }

        public Guid AddSource<T>(T amount) where T:ICurrency
        {
            switch (amount)
            {
                case Biofuel bf:
                {
                    var incomeSource = new BiofuelIncomeSource(bf, factionId);
                    incomeSources.Add(incomeSource.IncomeId, incomeSource);
                    totalIncomeRate += bf.Value;
                    IncomeChanged?.Invoke(totalIncomeRate);
                    return incomeSource.IncomeId;
                }
                case WarScrap ws:
                {
                    var incomeSource = new WarScrapIncomeSource(ws, factionId);
                    incomeSources.Add(incomeSource.IncomeId, incomeSource);
                    totalIncomeRate += ws.Value;
                    IncomeChanged?.Invoke(totalIncomeRate);
                    return incomeSource.IncomeId;
                }
                default:
                    throw new NotImplementedException(
                        $"[IncomeManager] AddSource is not implemented for the currency of type {typeof(T)}");
            }
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

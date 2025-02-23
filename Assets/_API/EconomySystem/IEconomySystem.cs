using System;
using System.Collections.Generic;
using System.Linq;
using Sisus.Init;
using UnityEngine;
using UnityEngine.Events;

namespace ADC.API
{
    public interface IEconomySystem
    {
        IFactionEconomy this[int factionId] { get; }
        public Dictionary<int, IFactionEconomy> FactionsEconomiesDictionary { get; }
    }

    public interface IFactionEconomy
    {
        public int FactionId { get; }
        void Init(IIncomeSourceFactory incomeSourceFactory, int factionId);
        void Start();
        void OnEnable();
        void OnDisable();
        bool Deposit<T>(T amount) where T: ICurrency;
        bool Withdraw<T>(T amount) where T : ICurrency;
        public IIncomeManager IncomeManager { get; }
        event EventHandler<CurrencyChangeEventArgs> CurrencyChanged;
    }

    [Serializable]
    public class FactionEconomiesCollection
    {
        [SerializeField] private Any<IFactionEconomy>[] factionEconomies;
        public IFactionEconomy[] FactionEconomies { get; set; }
        
        public FactionEconomiesCollection()
        {
            FactionEconomies = factionEconomies?.Select(fe=>fe.Value).ToArray();
        }
        
        public FactionEconomiesCollection(IFactionEconomy[] factionEconomies )
        {
            FactionEconomies = factionEconomies;
        }
        
    }
    
    public interface IIncomeManager
    {
        Guid AddSource<T>(T amount) where T : ICurrency;
        bool RemoveSource(Guid incomeId);

        public event EventHandler<IncomeEventArgs> IncomeReceived;
        UnityEvent<decimal> IncomeChanged { get; }
    }

    public interface IWaveTimer
    {
        event EventHandler<int> Begin;
        float Duration { get; }
        float Current { get; }
    }
    public interface IIncomeSourceFactory
    {
        IIncomeSource Create(ICurrency currency, int factionId);
    }

}

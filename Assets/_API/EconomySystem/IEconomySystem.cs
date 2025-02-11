using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sisus.Init;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ADC.API
{

    public abstract class CurrencyInterface : MonoBehaviour<IEconomySystem>
    {

        [SerializeField]
        [Tooltip("Left it to default -1 if it is belong to local faction")]
        private int factionId = -1;

        public int FactionId => factionId;

        
        protected override void Init(IEconomySystem economySystem)
        {
            economySystem.Add(this);
        }


        //protected virtual void Awake()
        //{
        //    EconomySystem.Instance.Add(this);
        //}
    }

    public interface IEconomySystem
    {
        IFactionEconomy this[int factionId] { get; }

        void Add(CurrencyInterface currencyInterface);

        public Dictionary<int, IFactionEconomy> FactionsEconomiesDictionary { get; }
    }

    public interface IFactionEconomy
    {
        public int FactionId { get; }
        void Init(IIncomeSourceFactory incomeSourceFactory, int factionId);
        void Start();
        //void AddVisualizer(CurrencyInterface visualizer);
        void OnEnable();
        void OnDisable();
        bool Deposit<T>(T amount) where T: ICurrency;
        bool Withdraw<T>(T amount) where T : ICurrency;
        public IIncomeManager IncomeManager { get; }
        void AddVisualizer(CurrencyInterface currencyInterface); //?
        public void AddVisualizers(IEnumerable<CurrencyInterface> visualizers);
    }

    [Serializable]
    public class FactionEconomiesCollection
    {
        [SerializeField] private Any<IFactionEconomy>[] factionEconomies;
        public IFactionEconomy[] FactionEconomies => factionEconomies.Select(fe=>fe.Value).ToArray();
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
        UnityEvent Begin { get; }
        float Duration { get; }
        float Current { get; }
    }
    public interface IIncomeSourceFactory
    {
        IncomeSource Create(ICurrency currency, int factionId);
    }

}

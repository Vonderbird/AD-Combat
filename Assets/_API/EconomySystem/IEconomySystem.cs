using System;
using System.Collections;
using System.Collections.Generic;
using Sisus.Init;
using UnityEngine;
using UnityEngine.Events;

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

    [Service]
    public interface IEconomySystem
    {
        IFactionEconomy this[int factionId] { get; }

        void Add(CurrencyInterface currencyInterface);

        public Dictionary<int, IFactionEconomy> FactionsEconomiesDictionary { get; }
    }

    public interface IFactionEconomy
    {
        void Init(IIncomeSourceFactory incomeSourceFactory, int factionId);
        void Start();
        //void AddVisualizer(CurrencyInterface visualizer);
        void OnEnable();
        void OnDisable();
        bool Deposit<T>(T amount) where T: ICurrency;
        bool Withdraw<T>(T amount) where T : ICurrency;
        public IIncomeManager IncomeManager { get; }
    }

    public interface IIncomeManager
    {
        Guid AddSource<T>(T amount) where T : ICurrency;
        bool RemoveSource(Guid incomeId);

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

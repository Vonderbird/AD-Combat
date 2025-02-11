using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using ADC.API;
using Sisus.Init;

namespace ADC.Currencies
{
    [Service(typeof(IEconomySystem), typeof(EconomySystem), FindFromScene = true)]
    public class EconomySystem : MonoBehaviour, IInitializable<FactionEconomiesCollection, IIncomeSourceFactory>, IEconomySystem
    {
        private IFactionEconomy[] _factionEconomies;
        public Dictionary<int, IFactionEconomy> FactionsEconomiesDictionary { get; private set; }

        private List<CurrencyInterface> GlobalVisualizers = new();

        private bool _isStarted = false;

        private IIncomeSourceFactory _incomeSourceFactory;

        // protected override void Init(IIncomeSourceFactory incomeSourceFactory)
        // {
        //     _incomeSourceFactory = incomeSourceFactory;
        // }
        //
        // public void Init(FactionEconomiesCollection factionEconomies)
        // {
        //     _factionEconomies = factionEconomies.FactionEconomies;
        //     for (var i = 0; i < _factionEconomies.Length; i++)
        //     {
        //         _factionEconomies[i].Init(_incomeSourceFactory, i);
        //         _factionEconomies[i].AddVisualizers(GlobalVisualizers);
        //     }
        //
        //     FactionsEconomiesDictionary =
        //         _factionEconomies.ToDictionary(faction => faction.FactionId, faction => faction as IFactionEconomy);
        //     _isStarted = true;
        //     OnEnable();
        // }
        //
        public void Init(FactionEconomiesCollection factionEconomies, IIncomeSourceFactory incomeSourceFactory)
        {
            _factionEconomies = factionEconomies.FactionEconomies;
            _incomeSourceFactory = incomeSourceFactory;
            for (var i = 0; i < _factionEconomies.Length; i++)
            {
                _factionEconomies[i].Init(incomeSourceFactory, i);
                _factionEconomies[i].AddVisualizers(GlobalVisualizers);
            }
        
            FactionsEconomiesDictionary =
                _factionEconomies.ToDictionary(faction => faction.FactionId, faction => faction as IFactionEconomy);
            _isStarted = true;
            OnEnable();
        }

        public IFactionEconomy this[int factionId]
        {
            get
            {
                if (FactionsEconomiesDictionary.TryGetValue(factionId, out var f))
                {
                    return f;
                }

                Debug.LogError($"[EconomySystem] there is no faction with id {factionId} in economy system!");
                return null;
            }
        }

        private void Start()
        {
            if (_factionEconomies == null)
            {
                Destroy(this);
                enabled = false;
                return;
            }
            foreach (var t in _factionEconomies)
                t.Start();
        }


        public void OnEnable()
        {
            if (!_isStarted) return;
            foreach (var factionEconomy in _factionEconomies)
                factionEconomy.OnEnable();
        }

        public void OnDisable()
        {
            if (!_isStarted) return;
            foreach (var factionEconomy in _factionEconomies)
                factionEconomy.OnDisable();
        }


        //public IGameManager GameMgr { get; private set; }

        public void Add(CurrencyInterface currencyInterface)
        {
            if (currencyInterface.FactionId == -1)
            {
                GlobalVisualizers.Add(currencyInterface);
            }
            else if (currencyInterface.FactionId < _factionEconomies.Length)
            {
                _factionEconomies[currencyInterface.FactionId].AddVisualizer(currencyInterface);
            }
            else
            {
                Debug.LogError($"[EconomySystem] FactionId {currencyInterface.FactionId} is not defined!");
            }

            if (_isStarted)
            {
                foreach (var t in _factionEconomies)
                {
                    t.AddVisualizer(currencyInterface);
                }
            }
        }

    }
}
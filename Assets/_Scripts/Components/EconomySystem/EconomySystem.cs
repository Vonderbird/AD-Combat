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

        private readonly List<CurrencyInterface> _globalVisualizers = new();

        private bool _isStarted;

        public void Init(FactionEconomiesCollection factionEconomies, IIncomeSourceFactory incomeSourceFactory)
        {
            _factionEconomies = factionEconomies.FactionEconomies;
            for (var i = 0; i < _factionEconomies.Length; i++)
            {
                _factionEconomies[i].Init(incomeSourceFactory, i);
                _factionEconomies[i].AddVisualizers(_globalVisualizers);
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


        public void Add(CurrencyInterface currencyInterface)
        {
            if (currencyInterface.FactionId == -1)
            {
                _globalVisualizers.Add(currencyInterface);
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
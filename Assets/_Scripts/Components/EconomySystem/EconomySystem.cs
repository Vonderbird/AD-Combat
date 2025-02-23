using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using ADC.API;
using Sisus.Init;

namespace ADC.Currencies
{
    [Service(typeof(IEconomySystem), typeof(EconomySystem), FindFromScene = true)]
    public class EconomySystem : MonoBehaviour, IInitializable<FactionEconomiesCollection, IIncomeSourceFactory>,
        IEconomySystem
    {
        private IFactionEconomy[] _factionEconomies;
        public Dictionary<int, IFactionEconomy> FactionsEconomiesDictionary { get; private set; }

        private bool _isStarted;

        public void Init(FactionEconomiesCollection factionEconomies, IIncomeSourceFactory incomeSourceFactory)
        {
            _factionEconomies = factionEconomies.FactionEconomies;
            for (var i = 0; i < _factionEconomies.Length; i++)
            {
                _factionEconomies[i].Init(incomeSourceFactory, i);
            }

            FactionsEconomiesDictionary =
                _factionEconomies.ToDictionary(faction => faction.FactionId, faction => faction as IFactionEconomy);
            _isStarted = true;
            OnEnable();
        }

        public IFactionEconomy this[int factionId] => FactionsEconomiesDictionary.GetValueOrDefault(factionId);

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

        private void OnEnable()
        {
            if (!_isStarted) return;
            foreach (var factionEconomy in _factionEconomies)
                factionEconomy.OnEnable();
        }

        private void OnDisable()
        {
            if (!_isStarted) return;
            foreach (var factionEconomy in _factionEconomies)
                factionEconomy.OnDisable();
        }
    }
}
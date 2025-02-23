using System;
using Sisus.Init;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

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
            // economySystem.Add(this);
            
            if(factionId==-1)
                foreach (var factionEconomy in economySystem.FactionsEconomiesDictionary.Values)
                    factionEconomy.CurrencyChanged += OnCurrencyChanged;
            else if(factionId<economySystem.FactionsEconomiesDictionary.Count)
                economySystem.FactionsEconomiesDictionary[factionId].CurrencyChanged += OnCurrencyChanged;
            else
            {
                throw new NotImplementedException(
                    $"economySystem.FactionsEconomiesDictionary doesn't have the target faction Id {factionId}");
            }
        }

        protected abstract void OnCurrencyChanged(object sender, CurrencyChangeEventArgs e);
    }
}
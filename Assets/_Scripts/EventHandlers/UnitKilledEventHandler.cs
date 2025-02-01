using ADC.API;
using ADC.Currencies;
using ADC.UnitCreation;
using RTSEngine.Health;
using UnityEngine;
using Zenject;

namespace ADC
{
    public class UnitKilledEventHandler : MonoBehaviour
    {
        private UnitHealth unitHealth;
        [SerializeField]
        private float killAwardPercent = 0.3f;
        private WarScrap killAward;
        private IEconomySystem economySystem;

        [Inject]
        public void Construct(IEconomySystem economySystem)
        {
            this.economySystem = economySystem;
        }

        private void Awake()
        {
            unitHealth = GetComponent<UnitHealth>();
            if(!unitHealth)
                Debug.LogError($"[UnitKilledEventHandler] must be attached to an entity that has a 'Unit Health' component.");
            var placementCost = GetComponent<UnitPlacementCosts>();
            killAward = placementCost.WarScrap * (decimal)killAwardPercent;
        }

        private void OnEnable()
        {
            unitHealth.DestroyState.triggerEvent.AddListener(OnUnitKilled);
        }

        private void OnDisable()
        {
            unitHealth.DestroyState.triggerEvent.RemoveListener(OnUnitKilled);
        }

        public void OnUnitKilled()
        {

            var predator = unitHealth.TerminatedBy;
            Debug.Log($">>>{predator.Name}<<<");
            economySystem.FactionsEconomiesDictionary[predator.FactionID].Deposit(killAward);
        }

    }
}

using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using RTSEngine.Event;
using RTSEngine.Health;
using UnityEngine;

namespace ADC
{
    public class RtsEngineInteractionManager : IThirdPartyInteractionManager
    {

        private readonly UnitAttack unitAttack;
        private readonly UnitHealth unitHealth;
        private readonly IUnit unit;

        public RtsEngineInteractionManager(UnitAttack unitAttack, UnitHealth unitHealth, IUnit unit)
        {
            this.unitAttack = unitAttack;
            this.unitHealth = unitHealth;
            this.unit = unit;
        }

        public void SetUnitArmor(int value)
        {
            Debug.LogError($"[RtsEngineInteractionManager] The method 'SetUnitArmor' is not implemented!");
        }

        public void SetUnitMaxHealth(int value)
        {
            Debug.Log($"unit: {unit}");
            unitHealth.SetMax(new HealthUpdateArgs(value, unit));
        }

        public void SetUnitDamage(int value)
        {
            var damageData = unitAttack.Damage.Data;
            damageData.unit = value;
            unitAttack.Damage.UpdateDamage(damageData);
        }

        public void SetBuildingDamage(int value)
        {
            var damageData = unitAttack.Damage.Data;
            damageData.building = value;
            unitAttack.Damage.UpdateDamage(damageData);
        }

        public void SetManaPoint(int value)
        {
            Debug.LogError($"[RtsEngineInteractionManager] The method 'SetManaPoint' is not implemented!");
        }
    }
}
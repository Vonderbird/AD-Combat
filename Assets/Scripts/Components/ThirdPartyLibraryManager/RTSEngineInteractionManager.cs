using System;
using ADC.API;
using RTSEngine.Attack;
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

        public event EventHandler<dynamic> TargetUpdated;

        public RtsEngineInteractionManager(UnitAttack unitAttack, UnitHealth unitHealth, IUnit unit)
        {
            this.unitAttack = unitAttack;
            this.unitHealth = unitHealth;
            this.unit = unit;

            unitAttack.TargetUpdated += OnTargetUpdated;


            #region For Information
            //// Unit UnitDamage Info and how to update it
            //unitAttack.Damage.UpdateDamage(new DamageData()
            //{
            //    building = currentSpecs.BuildingDamage,
            //    unit = currentSpecs.UnitDamage,
            //    custom = Array.Empty<CustomDamageData>()
            //});
            //// Unit Health Info and how to update it
            //unitHealth.SetMax(new RTSEngine.Event.HealthUpdateArgs(1000, unit));
            //unitHealth.SetMaxLocal(new RTSEngine.Event.HealthUpdateArgs(1000, unit));
            //unitHealth.Add(new RTSEngine.Event.HealthUpdateArgs(-1, unit));
            //Debug.Log($"unitHealth.CurrHealth: {unitHealth.CurrHealth}");
            //unitHealth.EntityDead += OnEntityDead;
            //unitHealth.EntityHealthUpdated += OnHealthUpdated;
            //unitHealth.EntityMaxHealthUpdated += OnMaxHealthUpdated;
            //Debug.Log($"unitHealth.IsDead: {unitHealth.IsDead}");
            //Debug.Log($"unitHealth.CanBeAttacked: {unitHealth.CanBeAttacked}");
            ////Debug.Log(unitHealth.DOTHandlers.First()?.UnitDamage);

            //unitHealth.Add(new RTSEngine.Event.HealthUpdateArgs(-1, unit));
            //Debug.Log($"unitHealth.EntityDead: {unitHealth.IsDead}");


            //// Unit Hit by Who for specifying attack and defence type
            //Debug.Log(unitAttack.Target.instance == null ? "is null" : unitAttack.Target.instance.Name);
            //unitHealth.AddDamageOverTime(
            //    new DamageOverTimeData { cycleDuration = 3, cycles = 10, infinite = false }
            //    , 5, unit);
            //Debug.Log(unitHealth.TerminatedBy?.Name);
            ////var attackDistanceHandler = unitAttack.AttackDistanceHandler as AttackFormationSelector;
            ////Debug.Log(attackDistanceHandler.MovementFormation.);
            //Debug.Log(unitAttack.CurrCooldownValue);
            //Debug.Log(unitAttack.Cooldown.CurrValue);
            //unitAttack.SetActive(false, true);
            //unitAttack.CooldownUpdated += CooldownUpdated;
            ////unitAttack.ActiveStatusUpdate
            //// Unit Attack What for specifying attack and defence type
            ////unitAttack.TargetUpdated;
            //// 

            #endregion
        }

        private void OnTargetUpdated(IEntityTargetComponent sender, TargetDataEventArgs args)
        {
            TargetUpdated?.Invoke(this, args.Data.instance);
        }


        public void SetUnitArmor(int value) { }

        public void SetUnitMaxHealth(int value)
        {
            unitHealth.SetMax(new HealthUpdateArgs(value, unit));
            if (unitHealth.CurrHealth > unitHealth.MaxHealth)
                unitHealth.Add(new HealthUpdateArgs(unitHealth.MaxHealth - unitHealth.CurrHealth, unit));
        }

        public void SetDamage(int unitDamage, int buildingDamage)
        {
            var damageData = new DamageData()
                { building = buildingDamage, unit = unitDamage, custom = Array.Empty<CustomDamageData>() };
            unitAttack.Damage.UpdateDamage(damageData);
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

        public void SetManaPoint(int value) { }
    }
}
using System;
using System.Collections.Generic;
using RTSEngine.Attack;
using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using RTSEngine.Event;
using RTSEngine.Health;
using UnityEngine;

namespace ADC
{
    [RequireComponent(typeof(IUnit))]
    public abstract class UnitBattleManager : MonoBehaviour
    {
        [SerializeField] protected UnitSpecs baseSpecs;
        protected UnitSpecs currentSpecs;
        protected IUnit unit { private set; get; }

        public EquipmentManager EquipmentManager { get; private set; }

        protected abstract List<ISpecialAbility> specialAbilities { get; set; }


        protected UnitAttack unitAttack;
        protected UnitHealth unitHealth;

        public UnitBattleManager Target { get; private set; }

        public UnitExperience XP { get; private set; }
        public UnitEquipments equipments;

        protected void Awake()
        {
            unit = GetComponent<Unit>();
            unitAttack = GetComponentInChildren<UnitAttack>();
            unitHealth = GetComponent<UnitHealth>();
            EquipmentManager = new EquipmentManager(unitAttack, unitHealth);
            currentSpecs = baseSpecs; // with updates and load from saved data
        }

        private void Start()
        {
            //// Unit UnitDamage Info and how to update it
            unitAttack.Damage.UpdateDamage(new DamageData()
            {
                building = currentSpecs.BuildingDamage,
                unit = currentSpecs.UnitDamage,
                custom = Array.Empty<CustomDamageData>()
            });
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
            unitAttack.TargetUpdated += OnTargetUpdated;
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
        }



        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }


        private void CooldownUpdated(IAttackComponent sender, EventArgs args)
        {
            Debug.LogError("Not Implemented");
            //throw new System.NotImplementedException();
        }

        private void OnMaxHealthUpdated(IEntity sender, HealthUpdateArgs args)
        {
            Debug.LogError("Not Implemented");
            //throw new System.NotImplementedException();
        }

        private void OnHealthUpdated(IEntity sender, HealthUpdateArgs args)
        {
            Debug.LogError("Not Implemented");
            //throw new System.NotImplementedException();
        }

        private void OnEntityDead(IEntity sender, DeadEventArgs args)
        {
            Debug.LogError("Not Implemented");
            //throw new System.NotImplementedException();
        }
        public void CalculateSpecs()
        {
            //defencePower = XP.Level * equipments.Power * 0.2f;
        }

        public abstract void Accept(IUnitManagerVisitor managerVisitor);

        public void OnAttackChanged()
        {

        }

        public void OnDefenceChanged()
        {

        }

        private void OnTargetUpdated(IEntityTargetComponent sender, TargetDataEventArgs args)
        {
            Debug.Log($"target args: {args.Data.instance.Name}");
            var target = args.Data.instance;
            if (target is IUnit u)
            {
                Target = u.GetComponent<UnitBattleManager>();
                Debug.Log($"target : {Target}");
                unitAttack.Damage.UpdateDamage(new DamageData()
                { building = 1000, unit = 1000, custom = Array.Empty<CustomDamageData>() });
            }
        }

       
    }

}
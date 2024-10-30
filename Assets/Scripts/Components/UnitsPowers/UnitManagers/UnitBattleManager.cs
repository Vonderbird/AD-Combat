using System;
using System.Collections.Generic;
using RTSEngine.Attack;
using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using RTSEngine.Event;
using RTSEngine.Health;
using UnityEngine;
using Object = System.Object;

namespace ADC
{
    [RequireComponent(typeof(IUnit))]
    public abstract class UnitBattleManager : MonoBehaviour
    {
        [SerializeField] protected UnitSpecs levelZeroSpecs;

        [SerializeField] protected UnitEquipments baseEquipments;
        
        protected IUnit unit { get; private set; }

        public EquipmentManager EquipmentManager { get; private set; }
        public UnitSpecsManager Specs { get; private set; }
        private IUnitSpecsCalculator unitSpecsCalculator;

        protected virtual List<ISpecialAbility> specialAbilities { get; set; } = new() { };
        protected int activeAbilityId = 0;
        public ISpecialAbility ActiveAbility => 
            specialAbilities is { Count: > 0 } ? specialAbilities[activeAbilityId] : null;

        private IThirdPartyInteractionManager thirdParty;
        //protected UnitAttack unitAttack;
        //protected UnitHealth unitHealth;

        public UnitBattleManager Target { get; private set; }

        public UnitExperience XP { get; private set; } = new();

        protected virtual void Awake()
        {
            unit = GetComponent<Unit>();
            var unitAttack = GetComponentInChildren<UnitAttack>();
            var unitHealth = GetComponent<UnitHealth>();
            thirdParty = new RtsEngineInteractionManager(unitAttack, unitHealth, unit);
            Specs = new UnitSpecsManager(thirdParty);
            EquipmentManager = new EquipmentManager();

            foreach (var specialAbility in specialAbilities)
            {
                XP.LevelChanged += specialAbility.OnLevelChanged;
            }
        }

        private void Start()
        {
            unitSpecsCalculator = new UnitSpecsCalculator(this);

            // 1. LoadData();
            // 2. XP.Level for base spaces upgrade!
            Specs.UpdateBaseSpecs(levelZeroSpecs);
            EquipmentManager.UpdateEquipments(Specs.BaseSpecs, baseEquipments);
            Specs.UpdateAdditiveSpecs(EquipmentManager.AddedSpecs);

            thirdParty.TargetUpdated += OnTargetUpdated;

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

        private void OnEnable()
        {
            EquipmentManager.EquipmentAdded += OnEquipmentAdded;
            EquipmentManager.EquipmentRemoved += OnEquipmentRemoved;
        }

        private void OnDisable()
        {
            EquipmentManager.EquipmentAdded -= OnEquipmentAdded;
            EquipmentManager.EquipmentRemoved -= OnEquipmentRemoved;
        }

        private void OnEquipmentAdded(object sender, EquipmentEventArgs e)
        {
            var unitSpecs = unitSpecsCalculator.CalculateAll();
            Specs.UpdateAdditiveSpecs(unitSpecs);

        }
        
        private void OnEquipmentRemoved(object sender, EquipmentEventArgs e)
        {
            var unitSpecs = unitSpecsCalculator.CalculateAll();
            Specs.UpdateAdditiveSpecs(unitSpecs);
        }

        public abstract void Accept(IUnitManagerVisitor managerVisitor);

        private void OnTargetUpdated(Object sender, dynamic t)
        {
            if (t is IUnit u)
            {
                Target = u.GetComponent<UnitBattleManager>();
                var (unitDmg, buildingDmg) = unitSpecsCalculator.CalculateDamage(Target);
                thirdParty.SetDamage(unitDmg, buildingDmg);
            }
            else
            {
                thirdParty.SetDamage(Specs.CurrentSpecs.UnitDamage, Specs.CurrentSpecs.BuildingDamage);
            }
        }

        public void OnSetActiveAbility(int id)
        {
            activeAbilityId = id;
        }

        public virtual void OnUseAbility()
        {
            ActiveAbility?.Use();
        }

        public virtual void OnUseAbility(int id)
        {
            if(specialAbilities==null ||  specialAbilities.Count<id+1) return;
            specialAbilities[id].Use();
        }


        #region Unused methods
        public void OnWeaponChanged()
        {

        }

        public void OnShieldChanged()
        {

        }

        public void CalculateSpecs()
        {
            //defencePower = XP.Level * equipments.Power * 0.2f;
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

        #endregion
    }

}
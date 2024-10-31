using System;
using System.Collections.Generic;
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


            thirdParty.TargetUpdated += OnTargetUpdated;
            Specs.BindEquipmentSpecs(EquipmentManager.AddedSpecs);

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
            Specs.BindEquipmentSpecs(EquipmentManager.AddedSpecs);

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
            Specs.BindEquipmentSpecs(unitSpecs);

        }
        
        private void OnEquipmentRemoved(object sender, EquipmentEventArgs e)
        {
            var unitSpecs = unitSpecsCalculator.CalculateAll();
            Specs.BindEquipmentSpecs(unitSpecs);
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
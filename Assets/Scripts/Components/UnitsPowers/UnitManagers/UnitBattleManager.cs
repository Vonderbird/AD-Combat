using System;
using System.Collections;
using System.Collections.Generic;
using ADC.API;
using ADC.UnitCreation;
using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using RTSEngine.Event;
using RTSEngine.Game;
using RTSEngine.Health;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ADC
{
    [RequireComponent(typeof(IUnit))]
    [RequireComponent(typeof(UnitSelectionCatch))]
    public abstract class UnitBattleManager : MonoBehaviour, IUnitBattleManager
    {

        [SerializeField] private DamageFactors damageFactors;

        //[SerializeField] protected UnitSpecs levelZeroSpecs;

        //[SerializeField] protected UnitEquipments baseEquipments;
        [SerializeField] protected SpecialAbilityBase[] specialAbilities;

        [SerializeField] private GameObject updateUIPrefab;
        protected IUnit unit { get; private set; }

        public IEquipmentManager EquipmentManager => equipmentManager;
        public abstract IUnitUpdateInfo UpdateInfo { get; }

        public IUnitSpecsManager Specs => specs;

        private BaseUnitSpecsCalculator unitSpecsCalculator;

        private List<ISpecialAbility> specialAbilitiesList;
        public virtual List<ISpecialAbility> SpecialAbilities
        {
            get
            {
                if (specialAbilitiesList != null) return specialAbilitiesList;
                specialAbilitiesList = new List<ISpecialAbility>(specialAbilities.Length);
                foreach (var specialAbility in specialAbilities)
                {
                    if (specialAbility == null)
                    {
                        Debug.LogError($"[UnitBattleManager] Special Ability on {name} is not assigned properly!");
                        continue;
                    }

                    specialAbilitiesList.Add(specialAbility.Initialize(this));
                    //Xp.Level.LevelChanged += specialAbility.OnLevelChanged;
                }
                return specialAbilitiesList;
            }
        }

        public Transform Transform => transform;
        public GameObject GameObject => gameObject;

        public T GetComponent<T>() where T : Object
        {
            return transform.GetComponent<T>();
        }

        public T GetComponentInChildren<T>() where T : Object
        {
            return transform.GetComponentInChildren<T>();
        }

        public T[] GetComponentsInChildren<T>() where T : Object
        {
            return transform.GetComponentsInChildren<T>();
        }

        protected int activeAbilityId = 0;
        public ISpecialAbility ActiveAbility =>
            SpecialAbilities is { Count: > 0 } ? specialAbilities[activeAbilityId] : null;

        private IThirdPartyInteractionManager thirdParty;
        [SerializeField] private UnitSpecsManager specs;
        [SerializeField] private EquipmentManager equipmentManager;

        public UnitBattleManager Target { get; private set; }

        public IUnitBattleManager CellUnit { get; private set; }

        public UnitExperience Xp { get; private set; } = new();

        public DamageFactors Factors => damageFactors;

        public GameObject UpdateUiPrefab => updateUIPrefab;

        private CellFillerComponent cellFillerComponent;

        protected virtual void Awake()
        {
            unit = GetComponent<Unit>();
            var unitAttack = GetComponentInChildren<UnitAttack>();
            var unitHealth = GetComponent<UnitHealth>();
            thirdParty = new RtsEngineInteractionManager(unitAttack, unitHealth, unit);
            Specs.Initialize(thirdParty);
            EquipmentManager.Initialize(this);

            thirdParty.TargetUpdated += OnTargetUpdated;  ////////////////////// issue in making damage zero
            Specs.BindEquipmentSpecs(EquipmentManager.AddedSpecs);
            cellFillerComponent = FindObjectOfType<CellFillerComponent>();
            CellUnit = cellFillerComponent.GetCorrespondingUnitCell(this);
            GetInfoFromUnitCell();
        }



        private void Start()
        {
            unitSpecsCalculator = FindObjectOfType<UnitSpecsCalculator>();

            // 1. LoadData();
            // 2. Xp.Level for base spaces upgrade!
            //Specs.BindEquipmentSpecs(EquipmentManager.AddedSpecs);

            Specs.UpdateBaseSpecs(Specs.BaseSpecs);
            EquipmentManager.UpdateEquipments(equipmentManager.Equipments);
            StartCoroutine(FreezeForSeconds(4.0f));
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


        //public abstract void Accept(IUnitManagerVisitor managerVisitor);

        private IEnumerator FreezeForSeconds(float seconds)
        {
            yield return null;
            if (unit == null)
            {
                Debug.LogError($"[UnitBattleManager] the unit field is not assigned!");
                yield break;
            }
            yield return new WaitUntil(() => unit.IsInitialized);
            if (unit.MovementComponent == null)
            {
                Debug.LogError($"[UnitBattleManager] the MovementComponent field is not implemented or assigned!");
                yield break;
            }
            var target = unit.MovementComponent.Target;
            unit.MovementComponent.SetActive(false, false);
            yield return new WaitForSeconds(seconds);
            unit.MovementComponent.SetActive(true, false);
            unit.MovementComponent.SetTarget(target, false);

            if (unit.AttackComponents[0].IsActive)
            {
                var abilityCount = SpecialAbilities.Count; // Initialize abilities!
            }
        }

        private void OnTargetUpdated(object sender, dynamic t)
        {
            if (t is IUnit u)
            {
                Target = u.GetComponent<UnitBattleManager>();
                //var (unitDmg, buildingDmg) = unitSpecsCalculator.CalculateDamage(this, Target);
                //thirdParty.SetDamage(unitDmg, buildingDmg);
            }
            //else
            //{
            //thirdParty.SetDamage(Specs.CurrentSpecs.UnitDamage, Specs.CurrentSpecs.BuildingDamage);
            //}
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
            if (SpecialAbilities == null || SpecialAbilities.Count < id + 1) return;
            SpecialAbilities[id].Use();
        }

        public virtual void OnUnitUpdate()
        {
            // ?
            // SpecialAbilities.ForEach(s=>s.UnlockLevel());
        }

        protected virtual void GetInfoFromUnitCell() { }

        #region Unused methods

        private void OnEquipmentAdded(object sender, EquipmentEventArgs e)
        {
            //var unitSpecs = unitSpecsCalculator.CalculateAll();
            //Specs.BindEquipmentSpecs(unitSpecs);

        }

        private void OnEquipmentRemoved(object sender, EquipmentEventArgs e)
        {
            //var unitSpecs = unitSpecsCalculator.CalculateAll();
            //Specs.BindEquipmentSpecs(unitSpecs);
        }

        public void OnWeaponChanged()
        {

        }

        public void OnShieldChanged()
        {

        }

        public void CalculateSpecs()
        {
            //defencePower = Xp.Level * equipments.Power * 0.2f;
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

    [Serializable]
    public class DamageFactors
    {
        [SerializeField] public float receiveRangeDamageFactor = 1.0f;

        public float ReceiveRangeDamageFactor => receiveRangeDamageFactor;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using RTSEngine;
using RTSEngine.Attack;
using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using RTSEngine.Event;
using RTSEngine.Health;
using UnityEngine;

namespace ADC
{

    [RequireComponent(typeof(IUnit))]
    public abstract class AttackManager : MonoBehaviour
    {
        [SerializeField] protected UnitSpecs baseSpecs;
        protected IUnit unit { private set; get; }

        protected abstract List<ISpecialAbility> specialAbilities { get; set; }

        public ArmorType Armor { get; private set; }
        public WeaponType Weapon { get; private set; }

        protected UnitAttack unitAttack;
        protected UnitHealth unitHealth;

        public AttackManager Target { get; private set; }

        public UnitExperience xp;
        public UnitEquipments equipments;

        protected void Awake()
        {
            unit = GetComponent<Unit>();
            unitAttack = GetComponentInChildren<UnitAttack>();
            unitHealth = GetComponent<UnitHealth>();
        }

        private void Start()
        {

            // Unit Damage Info and how to update it
            //unitAttack.Damage
            // Unit Health Info and how to update it
            unitHealth.SetMax(new RTSEngine.Event.HealthUpdateArgs(1000, unit));
            unitHealth.SetMaxLocal(new RTSEngine.Event.HealthUpdateArgs(1000, unit));
            unitHealth.Add(new RTSEngine.Event.HealthUpdateArgs(-1, unit));
            Debug.Log($"unitHealth.CurrHealth: {unitHealth.CurrHealth}");
            unitHealth.EntityDead += OnEntityDead;
            unitHealth.EntityHealthUpdated += OnHealthUpdated;
            unitHealth.EntityMaxHealthUpdated += OnMaxHealthUpdated;
            Debug.Log($"unitHealth.IsDead: {unitHealth.IsDead}");
            Debug.Log($"unitHealth.CanBeAttacked: {unitHealth.CanBeAttacked}");
            //Debug.Log(unitHealth.DOTHandlers.First()?.Damage);

            unitHealth.Add(new RTSEngine.Event.HealthUpdateArgs(-1, unit));
            Debug.Log($"unitHealth.EntityDead: {unitHealth.IsDead}");


            // Unit Hit by Who for specifying attack and defence type
            Debug.Log(unitAttack.Target.instance == null ? "is null" : unitAttack.Target.instance.Name);
            unitHealth.AddDamageOverTime(
                new DamageOverTimeData { cycleDuration = 3, cycles = 10, infinite = false }
                , 5, unit);
            Debug.Log(unitHealth.TerminatedBy?.Name);
            //var attackDistanceHandler = unitAttack.AttackDistanceHandler as AttackFormationSelector;
            //Debug.Log(attackDistanceHandler.MovementFormation.);
            Debug.Log(unitAttack.CurrCooldownValue);
            Debug.Log(unitAttack.Cooldown.CurrValue);
            unitAttack.SetActive(false, true);
            unitAttack.CooldownUpdated += CooldownUpdated;
            //unitAttack.ActiveStatusUpdate
            // Unit Attack What for specifying attack and defence type
            //unitAttack.TargetUpdated;
            // 
        }


        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }

        //private void Update()
        //{
        //}


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
            //defencePower = xp.Level * equipments.Power * 0.2f;
        }

        public abstract void Accept(IUnitManagerVisitor managerVisitor);

        public void OnAttackChanged()
        {
            var unitAttackData = unitAttack.Damage.Data;
            unitAttackData.unit = 13;
            unitAttackData.building = 13;
            //unitAttack.Damage.Data = unitAttackData;

        }

        public void OnDefenceChanged()
        {

        }

        // Call after transaction! or Inventory Equipment! or ...
        public void SetWeapon(WeaponType weapon)
        {
            Weapon = weapon;
            weaponEventArgs.WeaponType = weapon;
            WeaponChanged?.Invoke(this, weaponEventArgs);

            // set RTS-Engine weapon objects and settings
            // Modify RTS-Engine damage, health etc.
        }

        // Call after transaction! or Inventory Equipment! or ...
        public void SetArmor(ArmorType armor)
        {
            Armor = armor;
            armorEventArgs.ArmorType = armor;
            ArmorChanged?.Invoke(this, armorEventArgs);

            // set RTS-Engine armor objects and settings
            // Modify RTS-Engine damage, health etc.
        }

        private readonly WeaponTypeEventArgs weaponEventArgs = new();
        public event EventHandler<WeaponTypeEventArgs> WeaponChanged;

        private readonly ArmorTypeEventArgs armorEventArgs = new();
        public event EventHandler<ArmorTypeEventArgs> ArmorChanged;
    }

    public class UnitSpecsSetter
    {
        private readonly UnitHealth unitHealth;
        private readonly UnitAttack unitAttack;

        public UnitSpecsSetter(UnitHealth unitHealth, UnitAttack unitAttack)
        {
            this.unitHealth = unitHealth;
            this.unitAttack = unitAttack;
        }

        public void ChangeArmor(ArmorType armorType, WeaponType weaponType)
        {

        }
    }
}
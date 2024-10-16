using System.Collections.Generic;
using RTSEngine;
using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using RTSEngine.Health;
using UnityEngine;

namespace ADC
{

    [RequireComponent(typeof(IUnit))]
    public abstract class AttackManager:MonoBehaviour
    {
        [SerializeField] protected UnitSpecs baseSpecs;
        protected IUnit unit { private set; get; }

        protected abstract List<ISpecialAbility> specialAbilities { get; set; }

        public IDefenceType DefenceType { get; private set; }
        public IAttackType AttackType { get; private set; }
        public IArmorType ArmorType { private set; get; }
        public IWeaponType WeaponType { private set; get; }

        protected UnitAttack unitAttack;
        protected IUnitHealth unitHealth;

        public AttackManager Target { get; private set; }

        protected void Awake()
        {
            unit = GetComponent<Unit>();
            unitAttack = GetComponentInChildren<UnitAttack>();
            unitHealth = GetComponent<UnitHealth>();

            // Unit Damage Info and how to update it
            // Unit Health Info and how to update it
            // Unit Hit by Who for specifying attack and defence type
            // Unit Attack What for specifying attack and defence type
            // 
        }

        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }

        public UnitExperience xp;
        public UnitEquipments equipments;

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


    }
}
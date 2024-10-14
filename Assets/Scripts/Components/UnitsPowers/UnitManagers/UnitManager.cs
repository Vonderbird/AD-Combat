using System.Collections.Generic;
using RTSEngine;
using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using UnityEngine;

namespace ADC
{
    public abstract class UnitManager: FactionEntityTargetComponent<IFactionEntity>
    {
        [SerializeField] protected UnitSpecs baseSpecs;
        protected IUnit unit { private set; get; }

        protected abstract List<ISpecialAbility> specialAbilities { get; set; }

        protected override void OnTargetInit()
        {
            this.unit = factionEntity as IUnit;
        }

        protected IDefenceType defenceType;
        protected IAttackType attackType;

        public UnitExperience xp;
        public UnitEquipments equipments;

        public void CalculateSpecs()
        {
            //defencePower = xp.Level * equipments.Power * 0.2f;
        }

        public abstract void Accept(IUnitVisitor visitor);


        public override bool IsIdle { get; }
        public override bool IsTargetInRange(Vector3 sourcePosition, TargetData<IEntity> target)
        {
            throw new System.NotImplementedException();
        }

        public override ErrorMessage IsTargetValid(SetTargetInputData testInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
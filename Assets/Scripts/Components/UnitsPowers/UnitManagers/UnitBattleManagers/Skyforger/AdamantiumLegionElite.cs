using System.Collections.Generic;
using UnityEngine;

namespace ADC
{
    public class AdamantiumLegionElite : UnitBattleManager
    {
        protected override List<ISpecialAbility> specialAbilities { get; set; }


        protected override void Awake()
        {
            specialAbilities = new()
            {
                new AdvancingThePathway(this),
                new FlameWalker(this),
                new Tempest(this)
            };
            base.Awake();
        }

        public override void Accept(IUnitManagerVisitor managerVisitor)
        {
            managerVisitor.Visit(this);
        }

        public void TestTargetLock()
        {
            Debug.Log("Target Locked!");
        }
    }
}
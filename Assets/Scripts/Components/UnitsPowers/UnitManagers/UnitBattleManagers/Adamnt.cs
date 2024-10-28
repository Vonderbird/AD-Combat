using System.Collections.Generic;
using UnityEngine;

namespace ADC
{
    public class Adamnt : UnitBattleManager
    {
        protected override List<ISpecialAbility> specialAbilities { get; set; } = new()
        {
            new AdvancingThePathway(),
            new FlameWalker(),
            new Tempest()
        };


        protected override void Awake()
        {
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
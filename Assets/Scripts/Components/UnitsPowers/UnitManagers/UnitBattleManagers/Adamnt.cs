using System.Collections.Generic;
using UnityEngine;

namespace ADC
{
    public class Adamnt : UnitBattleManager
    {
        protected override List<ISpecialAbility> specialAbilities { get; set; } = new()
        {
        };

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
using System.Collections.Generic;
using UnityEngine;

namespace ADC
{
    public class Naloxian : UnitBattleManager
    {
        protected override List<ISpecialAbility> specialAbilities { get; set; } = new()
        {
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
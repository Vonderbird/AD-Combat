using ADC.API;
using System.Collections.Generic;
using UnityEngine;

namespace ADC
{
    public class Naloxian : UnitBattleManager
    {
        public override List<ISpecialAbility> specialAbilities { get; protected set; }

        protected override void Awake()
        {
            specialAbilities = new();
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
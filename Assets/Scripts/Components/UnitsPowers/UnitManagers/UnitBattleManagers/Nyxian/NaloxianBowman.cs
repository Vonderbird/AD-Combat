using ADC.API;
using System.Collections.Generic;
using UnityEngine;

namespace ADC
{
    public class NaloxianBowman : UnitBattleManager
    {
        //public override List<ISpecialAbility> SpecialAbilities { get; protected set; }

        //protected override void Awake()
        //{
        //    SpecialAbilities = new();
        //    base.Awake();
        //}

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
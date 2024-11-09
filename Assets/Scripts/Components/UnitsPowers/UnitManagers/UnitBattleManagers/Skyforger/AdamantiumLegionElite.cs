using System.Collections.Generic;
using ADC.API;
using UnityEngine;

namespace ADC
{
    public class AdamantiumLegionElite : UnitBattleManager
    {
        public override List<ISpecialAbility> specialAbilities { get; protected set; }


        protected override void Awake()
        {
            specialAbilities = new()
            {
                new AdamantiumArmor(this, 1),
                new BeamBlade(this, 1),
                new BrothersInArms(this, 2),
                new IronBreakerElite(this, 2),
                new AdvancedAdamantiumArmor(this, 2)
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
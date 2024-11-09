using ADC.API;
using System.Collections.Generic;

namespace ADC
{
    public class ThunderkinWarWagon : UnitBattleManager
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
    }
}
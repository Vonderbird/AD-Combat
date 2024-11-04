using System.Collections.Generic;

namespace ADC
{
    public class AdamantiumLegionSiegeBreaker : UnitBattleManager
    {
        protected override List<ISpecialAbility> specialAbilities { get; set; }


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
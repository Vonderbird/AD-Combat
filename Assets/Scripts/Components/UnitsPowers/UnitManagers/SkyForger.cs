using System.Collections.Generic;

namespace ADC
{
    public class SkyForger : UnitBattleManager
    {
        protected override List<ISpecialAbility> specialAbilities { get; set; }
        

        public override void Accept(IUnitManagerVisitor managerVisitor)
        {
            managerVisitor.Visit(this);
        }
    }
}
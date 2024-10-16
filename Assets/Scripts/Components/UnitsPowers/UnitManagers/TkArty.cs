using System.Collections.Generic;

namespace ADC
{
    public class TkArty : UnitManager
    {
        protected override List<ISpecialAbility> specialAbilities { get; set; } = new()
        {
            new AdvancingThePathway(),
            new FlameWalker()
        };

        public override void Accept(IUnitManagerVisitor managerVisitor)
        {
            managerVisitor.Visit(this);
        }
    }
}
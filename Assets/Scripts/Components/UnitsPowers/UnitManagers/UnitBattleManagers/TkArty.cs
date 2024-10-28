using System.Collections.Generic;

namespace ADC
{
    public class TkArty : UnitBattleManager
    {
        protected override List<ISpecialAbility> specialAbilities { get; set; } = new()
        {
            new AdvancingThePathway(),
            new FlameWalker()
        };

        protected override void Awake()
        {
            base.Awake();
        }

        public override void Accept(IUnitManagerVisitor managerVisitor)
        {
            managerVisitor.Visit(this);
        }
    }
}
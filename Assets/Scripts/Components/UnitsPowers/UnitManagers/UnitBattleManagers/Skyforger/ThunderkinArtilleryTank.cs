using System.Collections.Generic;

namespace ADC
{
    public class ThunderkinArtilleryTank : UnitBattleManager
    {
        protected override List<ISpecialAbility> specialAbilities { get; set; }

        protected override void Awake()
        {
            specialAbilities = new()
            {
                new AdvancingThePathway(this),
                new FlameWalker(this)
            };
            base.Awake();
        }

        public override void Accept(IUnitManagerVisitor managerVisitor)
        {
            managerVisitor.Visit(this);
        }
    }
}
using ADC.API;
using System.Collections.Generic;

namespace ADC
{
    public class ThunderkinArtilleryTank : UnitBattleManager
    {
        public override List<ISpecialAbility> specialAbilities { get; protected set; }

        protected override void Awake()
        {
            specialAbilities = new()
            {
                new Destroyer(this, 1),
                new ArtilleryUpgrade(this, 2)
            };
            base.Awake();
        }

        public override void Accept(IUnitManagerVisitor managerVisitor)
        {
            managerVisitor.Visit(this);
        }
    }
}
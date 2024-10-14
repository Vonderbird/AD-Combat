using System.Collections.Generic;

namespace ADC
{
    public class SkyForger : UnitManager
    {
        protected override List<ISpecialAbility> specialAbilities { get; set; }

        public override void Accept(IUnitVisitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}
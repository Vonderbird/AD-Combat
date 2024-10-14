using System.Collections.Generic;

namespace ADC
{
    public class TkArty : UnitManager
    {
        protected override List<ISpecialAbility> specialAbilities { get; set; }

        public override void Accept(IUnitVisitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}
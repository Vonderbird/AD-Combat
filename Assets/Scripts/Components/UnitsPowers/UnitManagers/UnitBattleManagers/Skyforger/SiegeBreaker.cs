using System.Collections.Generic;

namespace ADC
{
    public class SiegeBreaker : UnitBattleManager
    {
        protected override List<ISpecialAbility> specialAbilities { get; set; } = new() { };


        //protected override void Awake()
        //{
        //    base.Awake();
        //}

        public override void Accept(IUnitManagerVisitor managerVisitor)
        {
            managerVisitor.Visit(this);
        }
    }
}
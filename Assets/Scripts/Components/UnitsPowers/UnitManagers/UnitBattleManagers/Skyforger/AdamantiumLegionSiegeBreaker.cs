using System.Collections.Generic;
using ADC.API;

namespace ADC
{
    public class AdamantiumLegionSiegeBreaker : UnitBattleManager
    {
        //public override List<ISpecialAbility> SpecialAbilities { get; protected set; }


        //protected override void Awake()
        //{
        //    SpecialAbilities = new();
        //    base.Awake();
        //}

        public override void Accept(IUnitManagerVisitor managerVisitor)
        {
            managerVisitor.Visit(this);
        }
    }
}
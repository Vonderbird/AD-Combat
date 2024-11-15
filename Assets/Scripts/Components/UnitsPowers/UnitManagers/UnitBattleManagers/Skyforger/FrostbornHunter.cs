using ADC.API;
using System.Collections.Generic;

namespace ADC
{
    public class FrostbornHunter : UnitBattleManager
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
using ADC.API;
using ADC.Currencies;
using UnityEngine;

namespace ADC
{
    public class FrostbornIceStalker : UnitBattleManager
    {
        //public override List<ISpecialAbility> SpecialAbilities { get; protected set; }
        [SerializeField] private SimpleUnitUpdateInfo updateInfo;

        public override IUnitUpdateInfo UpdateInfo
        {
            get
            {
                updateInfo.OnUpdateAction ??= OnUnitUpdate;
                return updateInfo;
            }
        }


        //protected override void Awake()
        //{
        //    base.Awake();
        //}


        //public override void Accept(IUnitManagerVisitor managerVisitor)
        //{
        //    managerVisitor.Visit(this);
        //}
    }
}
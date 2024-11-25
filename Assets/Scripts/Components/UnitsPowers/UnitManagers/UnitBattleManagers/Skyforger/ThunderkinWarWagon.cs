using ADC.API;
using UnityEngine;

namespace ADC
{
    public class ThunderkinWarWagon : UnitBattleManager
    {
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
        //    UpdateInfo = new SimpleUnitUpdateInfo()
        //    {
        //        Cost = new(250),
        //        OnUpdateAction = OnUnitUpdate
        //    };
        //    base.Awake();
        //}

        public override void Accept(IUnitManagerVisitor managerVisitor)
        {
            managerVisitor.Visit(this);
        }
    }
}
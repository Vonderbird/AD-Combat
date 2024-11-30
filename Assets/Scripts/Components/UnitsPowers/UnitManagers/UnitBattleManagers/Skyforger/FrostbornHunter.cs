using ADC.API;
using UnityEngine;

namespace ADC
{


    public class FrostbornHunter : UnitBattleManager
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


        public override void Accept(IUnitManagerVisitor managerVisitor)
        {
            managerVisitor.Visit(this);
        }

    }
}
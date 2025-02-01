using ADC.API;
using ADC.Currencies;
using UnityEngine;

namespace ADC
{
    public class SplittingCobra : UnitBattleManager
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

        //public override void Accept(IUnitManagerVisitor managerVisitor)
        //{
        //    managerVisitor.Visit(this);
        //}

        public void TestTargetLock()
        {
            Debug.Log("Target Locked!");
        }
    }

    public class AdamantiumLegionElite : UnitBattleManager
    {
        [SerializeField] private SimpleUnitUpdateInfo updateInfo;


        protected override void Awake()
        {
            base.Awake();

        }

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

        public void TestTargetLock()
        {
            Debug.Log("Target Locked!");
        }
    }
}
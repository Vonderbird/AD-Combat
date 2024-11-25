using System;
using ADC.API;
using UnityEngine;

namespace ADC
{
    public enum FrostBornMode
    {
        Demolitionist,
        Grenade,
        EMP
    }

    public struct FrostbornHunterUpdateArgs : IUnitUpdateArgs
    {
        public FrostBornMode Mode { get; set; }
    }

    public class FrostbornHunter : UnitBattleManager
    {
        //public override List<ISpecialAbility> SpecialAbilities { get; protected set; }

        public FrostBornMode Mode { get; private set; }

        [SerializeField] private FreeMultiModelUpdateInfo updateInfo;
        public override IUnitUpdateInfo UpdateInfo
        {
            get
            {
                if (updateInfo.OnUpdateAction != null) return updateInfo;
                updateInfo.OnUpdateAction = OnUpdateMode;
                updateInfo.Modes = Enum.GetNames(typeof(FrostBornMode));
                updateInfo.ActiveMode = Enum.GetName(typeof(FrostBornMode), Mode);
                return updateInfo;
            }
        }


        //protected override void Awake()
        //{
        //    UpdateInfo = new FreeMultiModelUpdateInfo()
        //    {
        //        Modes = Enum.GetNames(typeof(FrostBornMode)),
        //        OnUpdateAction = OnUpdateMode
        //    };
        //    base.Awake();
        //}

        private void OnUpdateMode(string mode)
        {
            if (Enum.TryParse(typeof(FrostBornMode), mode, true, out var outMode))
            {
                Mode = (FrostBornMode)outMode;
            }
        }

        public override void Accept(IUnitManagerVisitor managerVisitor)
        {
            managerVisitor.Visit(this);
        }
    }
}
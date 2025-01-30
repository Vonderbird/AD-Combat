using ADC.API;
using RTSEngine.EntityComponent;
using System;
using System.Linq;
using UnityEngine;

namespace ADC
{
    [Serializable]
    public class UnitAttackMode
    {
        [SerializeField] private bool isActive;
        [SerializeField] private string name;
        [SerializeField] private UnitSpecs specs;
        [SerializeField] private UnitAttack unitAttack;

        public bool IsActive => isActive;
        public string Name => name;
        public UnitSpecs Specs => specs;
        public UnitAttack Attack => unitAttack;
    }

    public class ThunderkinDemolitionist : UnitBattleManager
    {
        //public override List<ISpecialAbility> SpecialAbilities { get; protected set; }
        [SerializeField] private string activeMode;
        public string ActiveMode { get => activeMode; private set => activeMode = value; }

        [SerializeField] private UnitAttackMode[] modes;

        private FreeMultiModelUpdateInfo updateInfo;

        public override IUnitUpdateInfo UpdateInfo
        {
            get
            {
                if (updateInfo == null || updateInfo.OnUpdateAction == null)
                {
                    updateInfo = new FreeMultiModelUpdateInfo
                    {
                        OnUpdateAction = OnUpdateMode,
                        Modes = modes.Select(m => m.Name).ToArray()
                    };
                }
                updateInfo.ActiveMode = string.IsNullOrEmpty(ActiveMode) ? modes.First(m => m.IsActive).Name : ActiveMode;
                return updateInfo;
            }
        }
        private void OnUpdateMode(string mode)
        {
            var modeInfo = modes.FirstOrDefault(m => m.Name == ActiveMode);
            //modeInfo?.Attack.SetActive(false, false);
            ActiveMode = mode;

            foreach (var attackMode in modes)
            {
                attackMode.Attack.SetActiveLocal(attackMode.Name == ActiveMode, false);
            }
            //modeInfo = modes.FirstOrDefault(m => m.Name == ActiveMode);
            //modeInfo?.Attack.SetActive(true, true);
            modeInfo?.Specs.Update(modeInfo.Specs);
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
        protected override void GetInfoFromUnitCell()
        {
            if (CellUnit is ThunderkinDemolitionist fh)
            {
                OnUpdateMode(fh.ActiveMode);

            }
        }

        //public override void Accept(IUnitManagerVisitor managerVisitor)
        //{
        //    managerVisitor.Visit(this);
        //}
    }
}
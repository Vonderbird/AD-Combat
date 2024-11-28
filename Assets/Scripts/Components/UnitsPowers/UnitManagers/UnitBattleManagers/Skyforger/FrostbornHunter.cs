using System;
using System.Linq;
using ADC.API;
using RTSEngine.EntityComponent;
using UnityEngine;

namespace ADC
{
    public enum FrostBornMode
    {
        Demolitionist,
        Grenade,
        EMP
    }

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

    public class FrostbornHunter : UnitBattleManager
    {
        //public override List<ISpecialAbility> SpecialAbilities { get; protected set; }
        public string ActiveMode { get; private set; }
        [SerializeField] private UnitAttackMode[] modes;

        private FreeMultiModelUpdateInfo updateInfo;
        public override IUnitUpdateInfo UpdateInfo
        {
            get
            {
                if (updateInfo != null) return updateInfo;
                updateInfo = new FreeMultiModelUpdateInfo();
                updateInfo.OnUpdateAction = OnUpdateMode;
                updateInfo.Modes = modes.Select(m => m.Name).ToArray();
                updateInfo.ActiveMode = string.IsNullOrEmpty(ActiveMode) ? modes.First(m => m.IsActive).Name : ActiveMode;
                return updateInfo;
            }
        }

        private void OnUpdateMode(string mode)
        {
            var modeInfo = modes.FirstOrDefault(m => m.Name == ActiveMode);
            modeInfo?.Attack.SetActive(false, false);
            ActiveMode = mode;
            modeInfo = modes.FirstOrDefault(m => m.Name == ActiveMode);
            modeInfo?.Attack.SetActive(true, true);
            modeInfo?.Specs.Update(modeInfo.Specs);
        }

        public override void Accept(IUnitManagerVisitor managerVisitor)
        {
            managerVisitor.Visit(this);
        }
    }
}
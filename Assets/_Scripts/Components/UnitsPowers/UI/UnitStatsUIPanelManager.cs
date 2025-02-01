using ADC.API;
using UnityEngine;

namespace ADC
{
    [RequireComponent(typeof(UnitStatsUIFiller))]
    public class UnitStatsUIPanelManager : Singleton<UnitStatsUIPanelManager>
    {
        private UnitUpdateUI unitUpdateUI;
        private UnitStatsUIOnOff unitStatsUiOnOff;
        private UnitStatsUIFiller unitStatsUiFiller;

        private void Awake()
        {
            unitStatsUiOnOff = GetComponentInChildren<UnitStatsUIOnOff>();
            unitUpdateUI = GetComponentInChildren<UnitUpdateUI>();
            unitStatsUiFiller = GetComponent<UnitStatsUIFiller>();
        }

        public void OnUnitSelected(UnitUIInfo unitUIInfo)
        {
            unitStatsUiOnOff.EnableButton();
            unitStatsUiOnOff.OpenPanel();
            unitStatsUiFiller.SetAll(unitUIInfo);
            unitUpdateUI.OnSelectUnit(unitUIInfo.UpdateInfo, unitUIInfo.Unit);
        }

        public void OnUnitDeselected(IUnitBattleManager unit)
        {
            unitStatsUiOnOff.DisableButton();
            unitStatsUiOnOff.ClosePanel();
        }


        public void OnDealStrikeDamage(int value)
        {
            // ????
        }

        public void OnReceiveStrikeDamage(int value)
        {
            // ???
        }

    }
}

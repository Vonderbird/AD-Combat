using ADC.API;
using UnityEngine;

namespace ADC
{
    [RequireComponent(typeof(UnitStatsUIFiller))]
    public class UnitStatsUIPanelManager : Singleton<UnitStatsUIPanelManager>
    {
        private UnitStatsUIOnOff unitStatsUiOnOff;
        private UnitStatsUIFiller unitStatsUiFiller;

        private void Awake()
        {
            unitStatsUiOnOff = GetComponentInChildren<UnitStatsUIOnOff>();
            unitStatsUiFiller = GetComponent<UnitStatsUIFiller>();
        }

        public void OnUnitSelected(UnitUIInfo unitUIInfo)
        {
            unitStatsUiOnOff.EnableButton();
            unitStatsUiOnOff.OpenPanel();
            unitStatsUiFiller.SetAll(unitUIInfo);
        }

        public void OnUnitDeselected(IUnitBattleManager unit)
        {
            unitStatsUiOnOff.DisableButton();
            unitStatsUiOnOff.ClosePanel();
        }

    }
}

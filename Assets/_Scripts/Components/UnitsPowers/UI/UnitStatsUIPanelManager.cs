using ADC.API;
using Sisus.Init;
using UnityEngine;

namespace ADC
{
    

    [Service]
    [RequireComponent(typeof(UnitStatsUIFiller))]
    public class UnitStatsUIPanelManager : MonoBehaviour, IUnitStatsUIPanelManager
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

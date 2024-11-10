using System.Collections;
using ADC.API;
using RTSEngine.Entities;
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

        }

        public void OnUnitDeselected(IEntity sender)
        {
            unitStatsUiOnOff.DisableButton();
            unitStatsUiOnOff.ClosePanel();
        }

    }
}

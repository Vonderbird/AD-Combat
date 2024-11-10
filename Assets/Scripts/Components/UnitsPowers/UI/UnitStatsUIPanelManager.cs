using System.Collections;
using UnityEngine;

namespace ADC
{
    [RequireComponent(typeof(UnitStatsUIFiller))]
    public class UnitStatsUIPanelManager : MonoBehaviour
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

        public void OnUnitDeselected()
        {
            unitStatsUiOnOff.DisableButton();
            unitStatsUiOnOff.ClosePanel();
        }

    }
}

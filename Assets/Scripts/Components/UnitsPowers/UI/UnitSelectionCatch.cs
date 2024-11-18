using ADC.API;
using RTSEngine.Selection;
using UnityEngine;
using SelectionType = ADC.API.SelectionType;

namespace ADC
{
    public class UnitSelectionCatch : UnitSelectionInfo
    {
        [SerializeField] private string title = "";
        [SerializeField] private Sprite unitBanner = null;

        public string Title
        {
            get
            {
                if (!string.IsNullOrEmpty(title)) return title;
                title = GetComponent<IUnitBattleManager>().GetType().Name;
                return title;
            }
        }

        public Sprite UnitBanner => unitBanner;


        private void Awake()
        {
            var unitSelection = GetComponentInChildren<UnitSelection>();
            unitSelection.Selected += (s, args) => OnUnitSelected(s,
                new SelectionEventArgs((SelectionType)args.Type, s.GetComponent<IUnitBattleManager>()));
            unitSelection.Deselected += (s, args) => OnUnitDeselected(s,
                new DeselectionEventArgs(s.GetComponent<IUnitBattleManager>()));
        }

        public override void OnUnitSelected(object sender, SelectionEventArgs args)
        {
            if (args.Type == SelectionType.single)
            {
                var uiInfo = ExtractUnitUIInfo(args.SelectedUnit);
                UnitStatsUIPanelManager.Instance.OnUnitSelected(uiInfo);
            }
        }
        public override void OnUnitDeselected(object sender, DeselectionEventArgs args)
        {
            UnitStatsUIPanelManager.Instance.OnUnitDeselected(args.SelectedUnit);
        }

    }
}

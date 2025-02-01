using ADC.API;
using ADC.UnitCreation;

namespace ADC
{
    public class UnitCellSelection : UnitSelectionInfo
    {
        private void Awake()
        {
            var cellFillerComponent = GetComponent<CellFillerComponent>();
            cellFillerComponent.UnitCellSelected += OnUnitSelected;
            cellFillerComponent.UnitCellDeselected += OnUnitDeselected;
            //var cellManager = cellFillerComponent.
            // OnUnitCellSelected
            // OnUnitCellDeselected

        }
        public override void OnUnitSelected(object sender, SelectionEventArgs args)
        {
            var uiInfo = ExtractUnitUIInfo(args.SelectedUnit);
            UnitStatsUIPanelManager.Instance.OnUnitSelected(uiInfo);

        }

        public override void OnUnitDeselected(object sender, DeselectionEventArgs args)
        {
            UnitStatsUIPanelManager.Instance.OnUnitDeselected(args.SelectedUnit);
        }

    }
}
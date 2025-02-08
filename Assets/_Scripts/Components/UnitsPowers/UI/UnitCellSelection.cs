using ADC.API;
using ADC.UnitCreation;
using Sisus.Init;

namespace ADC
{
    public class UnitCellSelection : UnitSelectionInfo, IInitializable<IUnitStatsUIPanelManager>
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
            _unitStatsUIPanelManager.OnUnitSelected(uiInfo);

        }

        public override void OnUnitDeselected(object sender, DeselectionEventArgs args)
        {
            _unitStatsUIPanelManager.OnUnitDeselected(args.SelectedUnit);
        }

        private IUnitStatsUIPanelManager _unitStatsUIPanelManager;
        public void Init(IUnitStatsUIPanelManager unitStatsUIPanelManager)
        {
            _unitStatsUIPanelManager = unitStatsUIPanelManager;
        }
    }
}
using RTSEngine.Entities;
using RTSEngine.Event;

namespace ADC
{
    public class UnitCellSelection : UnitSelectionInfo
    {
        public override void OnUnitSelected(IEntity sender, EntitySelectionEventArgs args)
        {
            UnitStatsUIPanelManager.Instance.OnUnitSelected(new UnitUIInfo());

            if (sender is IUnit unit)
            {
                var uiInfo = ExtractUnitUIInfo(unit);
            }
        }

        public override void OnUnitDeselected(IEntity sender, EntityDeselectionEventArgs args)
        {
            UnitStatsUIPanelManager.Instance.OnUnitDeselected(sender);
        }

    }
}
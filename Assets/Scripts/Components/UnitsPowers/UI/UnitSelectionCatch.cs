using RTSEngine.Entities;
using RTSEngine.Event;
using RTSEngine.Selection;
using UnityEngine;

namespace ADC
{
    public class UnitSelectionCatch : UnitSelectionInfo
    {
        private void Awake()
        {
            var unitSelection = GetComponentInChildren<UnitSelection>();
            unitSelection.Selected += OnUnitSelected;
            unitSelection.Deselected += OnUnitDeselected;
        }

        public override void OnUnitDeselected(IEntity sender, EntityDeselectionEventArgs args)
        {
            Debug.Log($"args: {args}");
            UnitStatsUIPanelManager.Instance.OnUnitDeselected(sender);
        }

        public override void OnUnitSelected(IEntity sender, EntitySelectionEventArgs args)
        {
            UnitStatsUIPanelManager.Instance.OnUnitSelected(new UnitUIInfo());
            Debug.Log($"args: {args}");
            if (sender is IUnit unit)
            {
                if (args.Type == SelectionType.single)
                {
                    var uiInfo = ExtractUnitUIInfo(unit);
                }
            }
        }
    }
}

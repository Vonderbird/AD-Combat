using RTSEngine.Entities;
using RTSEngine.Event;
using RTSEngine.Selection;
using UnityEngine;

namespace ADC
{
    public class UnitUIInfo
    {

    }

    public class UnitSelectionCatch : MonoBehaviour
    {
        private void Awake()
        {
            var unitSelection = GetComponentInChildren<UnitSelection>();
            unitSelection.Selected += OnUnitSelected;
            unitSelection.Deselected += OnUnitDeselected;
        }

        private void OnUnitDeselected(IEntity sender, EntityDeselectionEventArgs args)
        {
            Debug.Log($"args: {args}");
        }

        private void OnUnitSelected(IEntity sender, EntitySelectionEventArgs args)
        {
            Debug.Log($"args: {args}");
            if (sender is IUnit unit)
            {
                if (args.Type == SelectionType.single)
                {
                    var uiInfo = ExtractUnitUIInfo(unit);
                }
            }

        }

        private UnitUIInfo ExtractUnitUIInfo(IUnit unit)
        {
            return new UnitUIInfo();
        }
    }
}

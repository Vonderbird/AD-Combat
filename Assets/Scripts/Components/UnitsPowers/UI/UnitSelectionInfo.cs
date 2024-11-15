using RTSEngine.Entities;
using RTSEngine.Event;
using UnityEngine;

namespace ADC
{
    public struct UnitUIInfo
    {

    }
    public abstract class UnitSelectionInfo : MonoBehaviour
    {
        public abstract void OnUnitSelected(IEntity sender, EntitySelectionEventArgs args);
        public abstract void OnUnitDeselected(IEntity sender, EntityDeselectionEventArgs args);
        protected UnitUIInfo ExtractUnitUIInfo(IUnit unit)
        {
            return new UnitUIInfo();
        }
    }
}
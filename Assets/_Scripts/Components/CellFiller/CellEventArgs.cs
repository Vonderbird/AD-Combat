using UnityEngine;
using UnityEngine.EventSystems;

namespace ADC.UnitCreation
{
    public class CellEventArgs
    {
        public CellEventArgs(int cellId, PointerEventData eventData = null)
        {
            CellId = cellId;
            EventData = eventData;
        }

        public int CellId { get; }
        public Vector3 HitPoint { get; set; }
        public PointerEventData EventData { get; set; }
        public bool IsFilled { get; set; }
        public float UnitScaleFactor { get; set; }
    }
}
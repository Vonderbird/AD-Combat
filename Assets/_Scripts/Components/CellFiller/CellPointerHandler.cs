using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ADC.UnitCreation
{
    public interface ICellPointerHandler
    {
        bool HoverIsEnable { get; }
        UnityEvent<CellEventArgs> CellSelectiveClicked { get; }
        UnityEvent<CellEventArgs> CellDeletionClicked { get; }
        UnityEvent<CellEventArgs> CellSelectionEntered { get; }
        UnityEvent<CellEventArgs> CellDeletionEntered { get; }
        UnityEvent<CellEventArgs> CellExit { get; }
    }

    public class CellPointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
        IPointerMoveHandler, ICellPointerHandler
    {
        public bool IsBuildingSelected { get; set; } // ????
        public bool IsFilled { get; private set; } = false; // ????
        public DeleteButton DeleteButton { get; set; } // ????
        
        private CellEventArgs cellEventArgs;
        public UnityEvent<CellEventArgs> CellSelectiveClicked { get; private set; }
        public UnityEvent<CellEventArgs> CellDeletionClicked { get; private set; }
        public UnityEvent<CellEventArgs> CellSelectionEntered { get; private set; }
        public UnityEvent<CellEventArgs> CellDeletionEntered { get; private set; }
        public UnityEvent<CellEventArgs> CellExit { get; private set; }
        public bool HoverIsEnable { get; private set; } = false;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!IsBuildingSelected) return;

            cellEventArgs.HitPoint = GetPointerHitPoint(eventData);
            cellEventArgs.IsFilled = IsFilled;
            //Debug.Log($"Mouse Enter: {CellId}");
            if (DeleteButton && DeleteButton.IsDeleteEnabled)
                CellDeletionEntered?.Invoke(cellEventArgs);
            else
                CellSelectionEntered?.Invoke(cellEventArgs);
            HoverIsEnable = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!IsBuildingSelected) return;
            cellEventArgs.IsFilled = IsFilled;
            cellEventArgs.EventData = eventData;
            CellExit?.Invoke(cellEventArgs);
            HoverIsEnable = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            if (!IsBuildingSelected) return;

            cellEventArgs.EventData = eventData;
            cellEventArgs.IsFilled = IsFilled;
            if (DeleteButton && DeleteButton.IsDeleteEnabled)
            {
                CellDeletionClicked?.Invoke(cellEventArgs);
            }
            else
            {
                cellEventArgs.HitPoint = GetPointerHitPoint(eventData);
                //cellEventArgs.DecoObject = decoObject;
                CellSelectiveClicked?.Invoke(cellEventArgs);
            }
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (!IsBuildingSelected) return;

            cellEventArgs.HitPoint = GetPointerHitPoint(eventData);
            cellEventArgs.IsFilled = IsFilled;
            //Debug.Log($"Mouse Enter: {CellId}");
            if (DeleteButton && DeleteButton.IsDeleteEnabled)
                CellDeletionEntered?.Invoke(cellEventArgs);
            else
                CellSelectionEntered?.Invoke(cellEventArgs);
            HoverIsEnable = true;
        }
        
        private Vector3 GetPointerHitPoint(PointerEventData eventData)
        {
            Camera eventCamera = eventData.enterEventCamera;
            if (eventCamera == null)
            {
                Debug.LogWarning("No event camera found.");
                return Vector3.zero;
            }

            // Perform a raycast from the event camera to the pointer position
            Ray ray = eventCamera.ScreenPointToRay(eventData.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the raycast hit this object
                if (hit.collider.gameObject == gameObject)
                {
                    return hit.point; // Return the hit point on the object
                }
            }

            return Vector3.zero; // No hit, return zero vector
        }
    }
}
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using IUnit = RTSEngine.Entities.IUnit;

public class UnitCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private int materialId;

    private static int _incrementalId;
    public int CellId { get; } = _incrementalId++;

    private GameObject decoObject = null;
    private CellEventArgs cellEventArgs;
    public UnityEvent<CellEventArgs> CellAdditiveClicked;
    public UnityEvent<CellEventArgs> CellDeletionClicked;
    public UnityEvent<CellEventArgs> CellAdditiveEntered;
    public UnityEvent<CellEventArgs> CellDeletionEntered;
    public UnityEvent<CellEventArgs> CellExit;
    
    private Color cellDefaultColor;

    private bool hoverIsEnable = false;
    public bool IsBuildingSelected { get; set; }
    public DeleteButton DeleteButton { get; set; }

    public bool IsFilled => isFilled;

    private bool isFilled = false;


    void Awake()
    {
        cellEventArgs = new CellEventArgs(CellId);
        if (!renderer)
            renderer = GetComponentInChildren<Renderer>();

        cellDefaultColor = renderer.materials[materialId].color;

    }

    public void CreateDecoObject(IUnit unitPrefab, Vector3 position, float scaleFactor)
    {
        if (decoObject)
        {
            Debug.LogError($"Deco Object is not empty!");
            return;
        }

        decoObject = Instantiate(unitPrefab.Model, position, Quaternion.identity, transform);
        decoObject.transform.localScale = Vector3.one * scaleFactor;
    }
    public void ResetCell()
    {
        if (decoObject)
            Destroy(decoObject);
        isFilled = false;
        if (hoverIsEnable)
            renderer.materials[materialId].color = Color.green;
    }

    public void OnCellAdditiveEntered()
    {
        renderer.materials[materialId].color = decoObject == null ? Color.green : Color.gray;
    }

    public void OnCellDeletionEntered()
    {
        renderer.materials[materialId].color = decoObject == null ? Color.gray : Color.green;
    }

    public void OnCellExit()
    {
        renderer.materials[materialId].color = cellDefaultColor;
    }

    public void OnCellAdditiveClicked()
    {
        isFilled = true;
        renderer.materials[materialId].color = Color.red;
    }



    public void OnCellDeletionClicked()
    {
        ResetCell();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsBuildingSelected) return;

        cellEventArgs.HitPoint = GetPointerHitPoint(eventData);
        cellEventArgs.IsFilled = IsFilled;
        //Debug.Log($"Mouse Enter: {CellId}");
        if (DeleteButton && DeleteButton.IsDeleteEnabled)
            CellDeletionEntered?.Invoke(cellEventArgs);
        else
            CellAdditiveEntered?.Invoke(cellEventArgs);
        hoverIsEnable = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsBuildingSelected) return;
        cellEventArgs.IsFilled = IsFilled;
        cellEventArgs.eventData = eventData;
        CellExit?.Invoke(cellEventArgs);
        hoverIsEnable = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsBuildingSelected) return;

        cellEventArgs.eventData = eventData;
        cellEventArgs.IsFilled = IsFilled;
        if (DeleteButton && DeleteButton.IsDeleteEnabled)
        {
            CellDeletionClicked?.Invoke(cellEventArgs);
        }
        else
        {
            cellEventArgs.HitPoint = GetPointerHitPoint(eventData);
            //cellEventArgs.DecoObject = decoObject;
            CellAdditiveClicked?.Invoke(cellEventArgs);
        }
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

    public void OnPointerMove(PointerEventData eventData)
    {
        if (!IsBuildingSelected) return;

        cellEventArgs.HitPoint = GetPointerHitPoint(eventData);
        cellEventArgs.IsFilled = IsFilled;
        //Debug.Log($"Mouse Enter: {CellId}");
        if (DeleteButton && DeleteButton.IsDeleteEnabled)
            CellDeletionEntered?.Invoke(cellEventArgs);
        else
            CellAdditiveEntered?.Invoke(cellEventArgs);
        hoverIsEnable = true;
    }
}

public class CellEventArgs
{
    public CellEventArgs(int cellId, PointerEventData eventData = null)
    {
        CellId = cellId;
        this.eventData = eventData;
    }
    public int CellId { get; }
    public Vector3 HitPoint { get; set; }
    public PointerEventData eventData { get; set; }
    //public GameObject DecoObject { get; set; }
    public bool IsFilled { get; set; }
}
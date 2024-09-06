using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using IUnit = RTSEngine.Entities.IUnit;

public class UnitCell : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private int materialId;

    private static int _incrementalId;
    public int CellId { get; } = _incrementalId++;

    private GameObject decoObject = null;
    private CellClickedEventArgs cellClickedEventArgs;
    public UnityEvent<CellClickedEventArgs> CellClicked;
    public UnityEvent<int> CellRightClicked;

    private HashSet<GameObject> hitColliders;

    private bool isBuildingSelected;
    private Color cellDefaultColor;

    private bool hoverIsEnable = false;

    public bool IsBuildingSelected
    {
        get => isBuildingSelected;
        set
        {
            if (isBuildingSelected==value) return;
            isBuildingSelected = value;
            if(value)
                AddListeners();
            else
                RemoveListeners();
        }
    }

    void Awake()
    {
        hitColliders = GetComponentsInChildren<Collider>().Select((c)=> c.gameObject).ToHashSet();
        //Debug.Log($"Mouse Enter: {CellId}");
        cellClickedEventArgs = new CellClickedEventArgs(CellId);
        if (!renderer)
            renderer = GetComponentInChildren<Renderer>();
        cellDefaultColor = renderer.materials[materialId].color;

    }

    void OnEnable()
    {
        AddListeners();
    }

    void OnDisable()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        //CellPointerEvents.Instance.PointerEntered.AddListenerOnce(OnPointerEnter);
        //CellPointerEvents.Instance.PointerExited.AddListenerOnce(OnPointerExit);
        //CellPointerEvents.Instance.PointerClicked.AddListenerOnce(OnPointerClick);
    }

    private void RemoveListeners()
    {
        //if (!CellPointerEvents.Instance) return;
        //CellPointerEvents.Instance.PointerEntered.RemoveListener(OnPointerEnter);
        //CellPointerEvents.Instance.PointerExited.RemoveListener(OnPointerExit);
        //CellPointerEvents.Instance.PointerClicked.RemoveListener(OnPointerClick);
    }

    public void CreateDecoObject(IUnit unitPrefab)
    {
        if (decoObject)
        {
            Debug.LogError($"Deco Object is not empty!");
            return;
        }

        decoObject = Instantiate(unitPrefab.Model, transform);
    }
    public void ResetCell()
    {
        if (!decoObject) return;
        Destroy(decoObject);
        if(hoverIsEnable)
            renderer.materials[materialId].color = Color.green;
    }

    public void OnPointerEnter(PointerEventArgs eventData)
    {
        //Debug.Log($"{IsBuildingSelected}, {hitColliders.Contains(eventData.HitObject)}");
        if (!IsBuildingSelected || !hitColliders.Contains(eventData.HitObject)) return;
        //Debug.Log($"Mouse Enter: {CellId}");
        if (decoObject == null)
            renderer.materials[materialId].color = Color.green;
        else
            renderer.materials[materialId].color = Color.red;
        hoverIsEnable = true;
    }

    public void OnPointerExit(PointerEventArgs eventData)
    {
        if (!IsBuildingSelected || !hitColliders.Contains(eventData.HitObject)) return;
        renderer.materials[materialId].color = cellDefaultColor;
        hoverIsEnable = false;
    }

    public void OnPointerClick(PointerEventArgs eventData)
    {
        if (!IsBuildingSelected || !hitColliders.Contains(eventData.HitObject)) return;

        cellClickedEventArgs.DecoObject = decoObject;
        CellClicked?.Invoke(cellClickedEventArgs);
    }

    void OnMouseEnter()
    {
        if (!IsBuildingSelected) return;
        //Debug.Log($"Mouse Enter: {CellId}");
        if (decoObject == null)
            renderer.materials[materialId].color = Color.green;
        else
            renderer.materials[materialId].color = Color.red;
        hoverIsEnable = true;
    }

    void OnMouseExit()
    {
        if (!IsBuildingSelected) return;
        renderer.materials[materialId].color = cellDefaultColor;
        hoverIsEnable = false;
    }

    void OnMouseDown()
    {
        if (!IsBuildingSelected) return;
        renderer.materials[materialId].color = Color.red;

        cellClickedEventArgs.DecoObject = decoObject;
        CellClicked?.Invoke(cellClickedEventArgs);
    }
    void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(1))
        {
            CellRightClicked?.Invoke(CellId);
        }
    }
}

public class CellClickedEventArgs
{
    public CellClickedEventArgs(int cellId)
    {
        CellId = cellId;
    }
    public int CellId { get; }
    public GameObject DecoObject { get; set; }
}
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEngine.ParticleSystem;
using IUnit = RTSEngine.Entities.IUnit;

namespace ADC.UnitCreation
{

    public class UnitCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
        IPointerMoveHandler
    {
        [SerializeField] private Renderer cellRenderer;
        [SerializeField] private int materialId;
        [SerializeField] private float cellSizeFactor = 0.5f;
        public ParticleSystemGroup SpawnParticle { get; set; }
        public ParticleSystemGroup DeleteParticle { get; set; }

        [SerializeField] private Color defaultEmptyColor = Color.white;
        [SerializeField] private Color defaultFilledColor = Color.gray;


        private static int _incrementalId;
        public int CellId { get; } = _incrementalId++;

        private GameObject decoObject = null;
        private CellEventArgs cellEventArgs;
        public UnityEvent<CellEventArgs> CellAdditiveClicked;
        public UnityEvent<CellEventArgs> CellDeletionClicked;
        public UnityEvent<CellEventArgs> CellAdditiveEntered;
        public UnityEvent<CellEventArgs> CellDeletionEntered;
        public UnityEvent<CellEventArgs> CellExit;


        private bool hoverIsEnable = false;
        public bool IsBuildingSelected { get; set; }
        public DeleteButton DeleteButton { get; set; }

        public bool IsFilled => isFilled;

        private bool isFilled = false;


        void Awake()
        {
            cellEventArgs = new CellEventArgs(CellId);
            if (!cellRenderer)
                cellRenderer = GetComponentInChildren<Renderer>();

            cellRenderer.materials[materialId].color = IsFilled ? defaultFilledColor : defaultEmptyColor;
            ;
        }

        public void CreateDecoObject(IUnit unitPrefab, ParticleSystemGroup spawnParticle,
            ParticleSystemGroup deleteParticle, Vector3 position, float scaleFactor)
        {
            if (decoObject)
            {
                Debug.LogError($"Deco Object is not empty!");
                return;
            }

            decoObject = Instantiate(unitPrefab.Model, position, Quaternion.Euler(new Vector3(0, 90, 0)), transform);
            SpawnParticle = spawnParticle;
            DeleteParticle = deleteParticle;
            decoObject.transform.localScale = Vector3.one * scaleFactor;
        }

        public void ResetCell()
        {
            if (SpawnParticle)
                SpawnParticle = null;

            if (!isFilled) return;
            isFilled = false;
            if (hoverIsEnable)
                cellRenderer.materials[materialId].color = Color.green;

            if (DeleteParticle)
            {
                Debug.Log("Played Delete Particle");
                var deleteParticle = Instantiate(DeleteParticle, transform.position + Vector3.up * 0.25f,
                    Quaternion.identity, transform);
                deleteParticle.LifeSpan = 3f;
                deleteParticle.transform.localScale = Vector3.one * cellSizeFactor * deleteParticle.ScaleFactor;
                deleteParticle.Play();
                DeleteParticle = null;
            }

            StartCoroutine(DeleteDecoWithDelay(decoObject));
            decoObject = null;
        }

        private WaitForSeconds deleteDelay = new(1.5f);

        IEnumerator DeleteDecoWithDelay(GameObject tempObject)
        {
            yield return deleteDelay;

            if (tempObject)
                Destroy(tempObject);
        }

        public void OnCellAdditiveEntered()
        {
            cellRenderer.materials[materialId].color = decoObject == null ? Color.green : Color.gray;
        }

        public void OnCellDeletionEntered()
        {
            cellRenderer.materials[materialId].color = decoObject == null ? Color.gray : Color.green;
        }

        public void OnCellExit()
        {
            cellRenderer.materials[materialId].color = IsFilled ? defaultFilledColor : defaultEmptyColor;
        }

        public void OnCellAdditiveClicked()
        {
            isFilled = true;
            cellRenderer.materials[materialId].color = Color.red;
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

        public void OnCellUnitSpawned()
        {
            if (!isFilled) return;
            if (!SpawnParticle) return;
            var tempParticle = Instantiate(SpawnParticle, transform.position + Vector3.up * 0.25f, Quaternion.identity,
                transform);
            tempParticle.transform.localScale = Vector3.one * cellSizeFactor * tempParticle.ScaleFactor;
            tempParticle.LifeSpan = 3f;
            tempParticle?.Play();
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
        public float UnitScaleFactor { get; set; }
    }
}
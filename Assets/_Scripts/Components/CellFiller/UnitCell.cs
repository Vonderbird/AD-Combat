using System;
using System.Collections;
using System.Linq;
using ADC.API;
using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using RTSEngine.Health;
using RTSEngine.Selection;
using Sisus.Init;
using UnityEngine;
using IUnit = RTSEngine.Entities.IUnit;

namespace ADC.UnitCreation
{
    public class UnitCell : MonoBehaviour<ICellPointerHandler>
    {
        [SerializeField] private Renderer cellRenderer;
        [SerializeField] private int materialId;
        [SerializeField] private float cellSizeFactor = 0.5f;
        [SerializeField] private new Collider collider;
        private ICellPointerHandler _cellPointerHandler;
        private ParticlePlayer SpawnParticle;
        private ParticlePlayer DeleteParticle;
        private IVFXPoolingManager _vfxPoolingManager;

        [SerializeField] private Color defaultEmptyColor = Color.white;
        [SerializeField] private Color defaultFilledColor = Color.gray;

        private CellState cellState;
        public bool IsFilled { get; private set; } = false;
        public bool IsCellSelected { get; private set; } = false;

        private static int _incrementalId;
        public int CellId { get; } = _incrementalId++;

        private GameObject decoObject = null;

        public Collider Collider => collider;

        
        protected override void Init(ICellPointerHandler cellPointerHandler)
        {
            _cellPointerHandler = cellPointerHandler;
        }

        private void Start()
        {
            // cellEventArgs = new CellEventArgs(CellId);
            if (!cellRenderer)
                cellRenderer = GetComponentInChildren<Renderer>();

            UpdateCellState(false, false, PointerActionType.None);
        }

        #region Set state and color
        private void UpdateCellState(bool isFilled, bool isSelected, PointerActionType pointerActionType)
        {
            //if (IsCellSelected == isSelected && IsFilled == isFilled) return;
            IsCellSelected = isSelected;
            IsFilled = isFilled;
            cellState = isFilled
                ? (isSelected ? CellState.SelectedFilled : CellState.UnselectedFilled)
                : (isSelected ? CellState.SelectedUnfilled : CellState.UnselectedUnfilled);
            switch (pointerActionType)
            {
                case PointerActionType.None:
                    UpdateUnHoveredColor();
                    break;
                case PointerActionType.Additive:
                    UpdateHoveredAdditiveColor();
                    break;
                case PointerActionType.Selective:
                    UpdateHoveredSelectiveColor();
                    break;
                case PointerActionType.Deletive:
                    UpdateHoveredDeletiveColor();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pointerActionType), pointerActionType, null);
            }
        }

        private void UpdateHoveredAdditiveColor()
        {
            Color newColor = cellState switch
            {
                CellState.UnselectedUnfilled => Color.green,
                CellState.UnselectedFilled => Color.magenta,
                CellState.SelectedUnfilled => Color.green,
                CellState.SelectedFilled => Color.magenta,
                _ => defaultEmptyColor
            };

            cellRenderer.materials[materialId].color = newColor;
        }
        private void UpdateHoveredSelectiveColor()
        {
            Color newColor = cellState switch
            {
                CellState.UnselectedUnfilled => defaultEmptyColor,
                CellState.UnselectedFilled => Color.cyan,
                CellState.SelectedUnfilled => defaultEmptyColor,
                CellState.SelectedFilled => Color.magenta,
                _ => defaultEmptyColor
            };

            cellRenderer.materials[materialId].color = newColor;
        }
        private void UpdateHoveredDeletiveColor()
        {
            Color newColor = cellState switch
            {
                CellState.UnselectedUnfilled => defaultEmptyColor,
                CellState.UnselectedFilled => Color.red,
                CellState.SelectedUnfilled => defaultEmptyColor,
                CellState.SelectedFilled => Color.red,
                _ => defaultEmptyColor
            };

            cellRenderer.materials[materialId].color = newColor;
        }
        private void UpdateUnHoveredColor()
        {
            Color newColor = cellState switch
            {
                CellState.UnselectedUnfilled => defaultEmptyColor,
                CellState.UnselectedFilled => defaultFilledColor,
                CellState.SelectedUnfilled => defaultEmptyColor,
                CellState.SelectedFilled => Color.yellow,
                _ => defaultEmptyColor
            };

            cellRenderer.materials[materialId].color = newColor;
        }


        #endregion

        public IUnitBattleManager CreateDecoObject(IUnit unitPrefab, ParticlePlayer spawnParticle,
            ParticlePlayer deleteParticle, Vector3 position, float scaleFactor)
        {
            if (decoObject)
            {
                Debug.LogError($"Deco Object is not empty!");
                return null;
            }

            var unitObject = Instantiate(unitPrefab as Unit, position, Quaternion.Euler(new Vector3(0, -90, 0)), transform);
            decoObject = unitObject.gameObject;
            decoObject.GetComponent<IUnitHealth>().enabled = false;
            decoObject.GetComponentInChildren<UnitMovement>().enabled = false;
            decoObject.GetComponentInChildren<UnitSelection>().gameObject.SetActive(false);
            SpawnParticle = spawnParticle;
            DeleteParticle = deleteParticle;
            decoObject.transform.localScale = Vector3.one * scaleFactor;

            return decoObject.GetComponent<IUnitBattleManager>();
        }

        public void ResetCell()
        {
            if (SpawnParticle)
                SpawnParticle = null;

            if (!IsFilled) return;
            IsFilled = false;
            IsCellSelected = false;
            if (_cellPointerHandler.HoverIsEnable)
                cellRenderer.materials[materialId].color = Color.green;

            _vfxPoolingManager.SpawnVfx(DeleteParticle, transform.position + Vector3.up * 0.25f,
                Quaternion.identity, null);

            StartCoroutine(DeleteDecoWithDelay(decoObject));
            decoObject = null;
        }

        private WaitForSeconds deleteDelay = new(4.1f);

        IEnumerator DeleteDecoWithDelay(GameObject tempObject)
        {
            if (tempObject == null)
            {
                Debug.LogError($"[UnitCell.cs] tempObject cannot be null!");
                yield break;
            }
            var creationVFX = tempObject.transform.GetComponentsInChildren<CharacterCreationVFXs>().FirstOrDefault(c=>c.name=="Deletion");
            creationVFX?.OnDeleteCharacter();

            yield return deleteDelay;
            Debug.Log("Unit Deleted");

            if (tempObject)
                Destroy(tempObject);
        }

        public void OnCellAdditiveEntered() => UpdateCellState(IsFilled, IsCellSelected, PointerActionType.Additive);
        public void OnCellDeletionEntered()
        {
            UpdateCellState(IsFilled, IsCellSelected, PointerActionType.Deletive);
        }
        public void OnCellUnSelect()
        {
            IsCellSelected = false;
            UpdateCellState(IsFilled, IsCellSelected, PointerActionType.None);
        }
        public void OnCellExit() => UpdateCellState(IsFilled, IsCellSelected, PointerActionType.None);
        public void OnCellSelected()
        {
            IsCellSelected = true;
            UpdateCellState(IsFilled, IsCellSelected, PointerActionType.Selective);
        }
        public void OnCellAdditiveClicked()
        {
            IsFilled = true;
            UpdateCellState(IsFilled, IsCellSelected, PointerActionType.Additive);
        }
        public void OnCellDeletionClicked() => ResetCell();
        public void OnCellUnitSpawned()
        {
            if (!IsFilled) return;
            if (!SpawnParticle) return;
            _vfxPoolingManager.SpawnVfx(SpawnParticle, transform.position + Vector3.up * 0.25f,
                Quaternion.identity, null);
        }
        private enum CellState
        {
            UnselectedUnfilled,
            UnselectedFilled,
            SelectedUnfilled,
            SelectedFilled
        }
        private enum PointerActionType
        {
            None,
            Additive,
            Selective,
            Deletive
        }

    }
}
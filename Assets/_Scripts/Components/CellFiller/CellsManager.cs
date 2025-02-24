using System;
using System.Collections.Generic;
using System.Linq;
using ADC.API;
using UnityEngine;
using ADC.Currencies;
using RTSEngine.Event;
using RTSEngine.Entities;
using UnityEngine.Events;
using JetBrains.Annotations;
using System.Collections;

namespace ADC.UnitCreation
{
    public class CellsManager
    {
        private Transform cellsParent;
        private readonly CellUnitSpawner unitSpawner;
        private readonly ActiveTaskContainer activeTask;
        private readonly IIncomeManager incomeManager;
        private readonly Dictionary<int, UnitCell> unitCells = new();
        private readonly Dictionary<int, Vector3> cellPositions = new();

        private Dictionary<int, List<int>> unitCellsGroups = new();
        private readonly Dictionary<int, Vector3> unitsGroupPosition = new();
        private readonly Dictionary<int, int> cellGroupIds = new();
        public Dictionary<int, int> CellGroupIds => cellGroupIds;
        private readonly Dictionary<int, IUnitBattleManager> groupUnitDeco = new();
        private readonly Dictionary<int, IUnit> groupUnitPrefab = new();
        public Dictionary<int, IUnitBattleManager> GroupUnitDeco => groupUnitDeco;
        public Dictionary<int, IUnit> GroupUnitPrefab => groupUnitPrefab;
        private int selectedUnitGroup;

        public UnityEvent<CellEventArgs> AdditiveCellClicked = new();
        public UnityEvent<CellEventArgs> DeletionCellClicked = new();

        public EventHandler<SelectionEventArgs> UnitCellSelected;
        public EventHandler<DeselectionEventArgs> UnitCellDeselected;

        private readonly UnitPlacementTransactionLogic unitPlacementTransaction;
        private readonly UnitDeletionTransactionLogic unitDeletionTransaction;
        
        private readonly int factionId;
        /// <summary>
        /// Key(int): ?,
        /// Value(UnitCell): ?
        /// </summary>
        public Dictionary<int, UnitCell> UnitCells => unitCells;

        /// <summary>
        /// Key(int): ?,
        /// Value(Vector3): ?
        /// </summary>
        public Dictionary<int, Vector3> UnitsGroupPosition => unitsGroupPosition;

        /// <summary>
        /// Key(int): Cell groupId,
        /// Value(Guid): Income source id
        /// </summary>
        private Dictionary<int, Guid> unitIncomeSources = new();

        /// <summary>
        /// Key(int): Cell groupId,
        /// Value(IUnit): Unit's prefab
        /// </summary>
        private Dictionary<int, IUnit> positionedUnitsPrefabs = new();

        private Coroutine updateCoroutine;

        private IEconomySystem economySystem;
        private readonly ICellPointerHandler[] _cellPointerHandlers;


        public CellsManager(IEconomySystem economySystem, ICellPointerHandler[] cellPointerHandlers, [NotNull] Transform cellsParent, [NotNull] CellUnitSpawner unitSpawner,
            [NotNull] ActiveTaskContainer activeTask, int factionId)
        {
            this.factionId = factionId;
            this.economySystem = economySystem;
            _cellPointerHandlers = cellPointerHandlers;
            this.cellsParent = cellsParent ?? throw new ArgumentNullException(nameof(cellsParent));
            this.unitSpawner = unitSpawner ?? throw new ArgumentNullException(nameof(unitSpawner));
            this.activeTask = activeTask ?? throw new ArgumentNullException(nameof(activeTask));
            incomeManager = economySystem.FactionsEconomiesDictionary[factionId].IncomeManager;
            unitSpawner.OnUnitsSpawned.AddListener(OnCellUnitSpawned);
            
            unitPlacementTransaction = new UnitPlacementTransactionLogic(economySystem, factionId);
            unitDeletionTransaction = new UnitDeletionTransactionLogic(economySystem, factionId);
        }

        public void OnEnabled(DeleteButton deleteButton)
        {
            PrepareCells();

            foreach (var pointerHandler in _cellPointerHandlers)
            {
                pointerHandler.CellSelectionEntered.AddListener(OnCellSelectionEntered);
                pointerHandler.CellDeletionEntered.AddListener(OnCellDeletionEntered);
                pointerHandler.CellExit.AddListener(OnCellExit);
                pointerHandler.CellSelectiveClicked.AddListener(OnCellSelectiveClicked);
                pointerHandler.CellDeletionClicked.AddListener(OnCellDeletionClicked);
                pointerHandler.DeleteButton = deleteButton;
            }

            if (unitSpawner)
                unitSpawner.OnUnitsSpawned.AddListener(OnCellUnitSpawned);
            updateCoroutine ??= unitSpawner.StartCoroutine(Update());
        }

        public void OnDisabled()
        {
            foreach (var pointerHandler in _cellPointerHandlers)
                pointerHandler.CellSelectiveClicked.RemoveListener(OnCellSelectiveClicked);

            if (unitSpawner)
                unitSpawner.OnUnitsSpawned.RemoveListener(OnCellUnitSpawned);

            if (updateCoroutine != null && unitSpawner)
                unitSpawner.StopCoroutine(updateCoroutine);
        }

        IEnumerator Update()
        {
            while (true)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    OnAllCellsUnselect(null);
                }

                yield return null;
            }
        }

        private void OnCellSelectionEntered(CellEventArgs arg0)
        {
            if (activeTask.HasValue)
                OnCellAdditiveEnterd(arg0);
            else
                OnFilledCellSelectionEntered(arg0);

        }

        private void OnCellAdditiveEnterd(CellEventArgs arg0)
        {
            if (arg0.IsFilled || !activeTask.HasValue) return;
            OnCellExit(arg0);

            var taskPopulation = 0;
            foreach (var requiredResource in activeTask.UnitCreationTask.RequiredResources)
            {
                if (requiredResource.type.name == "population")
                {
                    taskPopulation = requiredResource.value.amount;
                }
            }

            var nearestCellIds = cellPositions
                .Where(p => !UnitCells[p.Key].IsFilled)
                .OrderBy(p => Vector3.Distance(arg0.HitPoint, p.Value))
                .Take(taskPopulation)
                .Select(p => p.Key)
                .ToList();
            if (nearestCellIds.Count < taskPopulation) return;
            foreach (var cellId in nearestCellIds)
                UnitCells[cellId].OnCellAdditiveEntered();
        }

        private void OnFilledCellSelectionEntered(CellEventArgs arg0)
        {
            if (!arg0.IsFilled || activeTask.HasValue) return;

            if (!cellGroupIds.TryGetValue(arg0.CellId, out var cellGroup)) return;
            if (!unitCellsGroups.TryGetValue(cellGroup, out var cellIds))
            {
                Debug.LogError($"[CellManager] The unitCellsGroups did not sync with cellGroupIds");
                return;
            }

            foreach (var cellId in cellIds) 
                UnitCells[cellId].OnCellAdditiveEntered();
        }

        private void OnCellDeletionEntered(CellEventArgs arg0)
        {
            OnCellExit(arg0);
            if (!cellGroupIds.TryGetValue(arg0.CellId, out var cellGroup)) return;
            if (!unitCellsGroups.TryGetValue(cellGroup, out var cellIds))
            {
                Debug.LogError($"[CellManager] The unitCellsGroups did not sync with cellGroupIds");
                return;
            }

            foreach (var cellId in cellIds)
                UnitCells[cellId].OnCellDeletionEntered();
        }

        private void OnCellExit(CellEventArgs arg0)
        {
            foreach (var (_, unitCell) in UnitCells)
            {
                unitCell.OnCellExit();
            }
        }

        public void OnCellSelectiveClicked(CellEventArgs arg0)
        {
            if (activeTask.HasValue)
                OnCellAdditiveClicked(arg0);
            else
                OnFilledCellSelectionClicked(arg0);
        }

        private void OnFilledCellSelectionClicked(CellEventArgs arg0)
        {
            if(!arg0.IsFilled || activeTask.HasValue) return;

            if (!cellGroupIds.TryGetValue(arg0.CellId, out var cellGroup)) return;
            if(selectedUnitGroup == cellGroup) return;
            OnAllCellsUnselect(arg0);

            if (!unitCellsGroups.TryGetValue(cellGroup, out var cellIds))
            {
                Debug.LogError($"[CellManager] The unitCellsGroups did not sync with cellGroupIds");
                return;
            }

            foreach (var cellId in cellIds)
                UnitCells[cellId].OnCellSelected();
            if (!groupUnitDeco.TryGetValue(cellGroup, out var unitDeco))
            {
                Debug.LogError($"[CellManager] cellGroup is not in the groupUnit!");
                return;
            }
            selectedUnitGroup = cellGroup;
            UnitCellSelected?.Invoke(this, new SelectionEventArgs(SelectionType.single, unitDeco));
        }

        public void OnAllCellsUnselect(CellEventArgs arg0)
        {
            //var cellIds = new HashSet<int>(unitCellsGroups.ContainsKey(selectedUnitGroup)? 
            //    unitCellsGroups[selectedUnitGroup]: new List<int>());

            foreach (var (_, unitCell) in UnitCells)
                unitCell.OnCellUnSelect();
            if (!groupUnitDeco.TryGetValue(selectedUnitGroup, out var unitDeco)) return;
            UnitCellDeselected?.Invoke(this, new DeselectionEventArgs(unitDeco));
            selectedUnitGroup = -1;
        }
        private void OnCellAdditiveClicked(CellEventArgs arg0)
        {
            if (arg0.IsFilled || !activeTask.HasValue) return;
            if (cellGroupIds.ContainsKey(arg0.CellId))
            {
                Debug.LogError($"[CellManager] cellGroupIds already has the cell Id {arg0.CellId}!");
                return;
            }

            var taskPopulation = 0;
            foreach (var requiredResource in activeTask.UnitCreationTask.RequiredResources)
            {
                if (requiredResource.type.name == "population")
                {
                    taskPopulation = requiredResource.value.amount;
                }
            }

            var nearestCellIds = cellPositions
                .Where(p => !UnitCells[p.Key].IsFilled)
                .OrderBy(p => Vector3.Distance(arg0.HitPoint, p.Value))
                .Take(taskPopulation)
                .Select(p => p.Key)
                .ToList();
            if (nearestCellIds.Count < taskPopulation) return;

            var unitToSpawn = this.activeTask.UnitCreationTask.TargetObject;
            var unitPlacementCosts = unitToSpawn.GetComponent<UnitPlacementCosts>();
            if (unitPlacementCosts)
                if (!unitPlacementTransaction.Process(unitPlacementCosts)) return;

            arg0.UnitScaleFactor = 1 + MathF.Log(taskPopulation);

            int groupId = nearestCellIds.First();
            unitCellsGroups.Add(groupId, new List<int>(taskPopulation));
            Vector3 averagePosition = Vector3.zero;
            foreach (var cellId in nearestCellIds)
            {
                averagePosition += cellPositions[cellId];
                UnitCells[cellId].OnCellAdditiveClicked();
                unitCellsGroups[groupId].Add(cellId);
                cellGroupIds.Add(cellId, groupId);
            }
            averagePosition /= taskPopulation;
            UnitsGroupPosition[groupId] = averagePosition;


            var unitDeco = UnitCells[groupId].CreateDecoObject(
                activeTask.UnitCreationTask.TargetObject,
                activeTask.UnitCreationTask.SpawnParticleSystem,
                activeTask.UnitCreationTask.DeletionParticleSystem,
                averagePosition,
                arg0.UnitScaleFactor);





            groupUnitDeco.Add(groupId, unitDeco);
            groupUnitPrefab.Add(groupId, activeTask.UnitCreationTask.TargetObject);

            AdditiveCellClicked?.Invoke(arg0);

            var incomeSourceId = incomeManager.AddSource(
                unitPlacementCosts.WarScrap * unitPlacementCosts.IncomeRatio);
            unitIncomeSources.Add(groupId, incomeSourceId);
            positionedUnitsPrefabs.Add(groupId, unitToSpawn);
        }

        private void OnCellDeletionClicked(CellEventArgs arg0)
        {
            CoroutinePlayer.Instance.StartCoroutine(StartCellUnitDeactivation(arg0));
        }


        private WaitForSeconds waitForDeletion = new(4);
        IEnumerator StartCellUnitDeactivation(CellEventArgs arg0)
        {
            if (!cellGroupIds.TryGetValue(arg0.CellId, out var cellGroup)) yield break;
            if (!unitCellsGroups.TryGetValue(cellGroup, out var cellIds))
            {
                Debug.LogError($"[CellManager] The unitCellsGroups did not sync with cellGroupIds");
                yield break;
            }


            var unitToDelete = positionedUnitsPrefabs[cellGroup];
            var unitPlacementCosts = unitToDelete.GetComponent<UnitPlacementCosts>();
            if (unitPlacementCosts)
                if (!unitDeletionTransaction.Process(unitPlacementCosts)) yield break;

            //Transform unitTransform = unitToDelete.transform;
            DisableSelection(cellIds);
            yield return waitForDeletion;
            incomeManager.RemoveSource(unitIncomeSources[cellGroup]);
            unitIncomeSources.Remove(cellGroup);
            groupUnitDeco.Remove(cellGroup);
            groupUnitPrefab.Remove(cellGroup);

            DeletionCellClicked?.Invoke(arg0);
            foreach (var cellId in cellIds)
            {
                UnitCells[cellId].OnCellDeletionClicked();
                unitSpawner.RemoveUnit(cellId);
                cellGroupIds.Remove(cellId);
            }

            unitCellsGroups.Remove(cellGroup);
            positionedUnitsPrefabs.Remove(cellGroup);
            EnableSelection(cellIds);
        }

        private void DisableSelection(List<int> cellIds)
        {
            foreach (var cellId in cellIds)
            {
                UnitCells[cellId].Collider.enabled = false;
            }
        }

        private void EnableSelection(List<int> cellIds)
        {
            foreach (var cellId in cellIds)
            {
                UnitCells[cellId].Collider.enabled = true;
            }
        }
        public void OnCellUnitSpawned(UnitsSpawnEventArgs spawnEventArgs)
        {
            //cellGroupIds.Clear();
            //unitCellsGroups.Clear();
            foreach (var (cellId, unitCell) in UnitCells)
            {
                unitCell.OnCellUnitSpawned();
            }
        }

        public void PrepareCells()
        {
            if (UnitCells.Count > 0) return;

            var unitCellsArray = cellsParent.GetComponentsInChildren<UnitCell>();
            foreach (var unitCell in unitCellsArray)
            {
                UnitCells.Add(unitCell.CellId, unitCell);
                cellPositions.Add(unitCell.CellId, unitCell.transform.position);
            }
        }

        public void OnEntitySelected(IEntity sender, EntitySelectionEventArgs args)
        {
            foreach (var (_, unitCell) in UnitCells)
            {
                unitCell.IsBuildingSelected = true;
            }
        }

        public void OnEntityDeselected(IEntity sender, EntityDeselectionEventArgs args)
        {
            foreach (var (_, unitCell) in UnitCells)
            {
                unitCell.IsBuildingSelected = false;
            }
        }

    }
}
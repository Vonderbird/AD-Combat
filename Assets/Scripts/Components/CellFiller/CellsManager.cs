using System;
using System.Collections.Generic;
using System.Linq;
using ADC.Currencies;
using JetBrains.Annotations;
using RTSEngine.Entities;
using RTSEngine.Event;
using UnityEngine;
using UnityEngine.Events;

namespace ADC.UnitCreation
{

    public class CellsManager
    {
        private Transform cellsParent;
        private readonly CellUnitSpawner unitSpawner;
        private readonly ActiveTaskContainer activeTask;
        private readonly IncomeManager incomeManager;
        private readonly Dictionary<int, UnitCell> unitCells = new();
        private readonly Dictionary<int, Vector3> cellPositions = new();

        private Dictionary<int, List<int>> unitCellsGroups = new();
        private readonly Dictionary<int, int> cellGroupIds = new();

        public UnityEvent<CellEventArgs> AdditiveCellClicked = new();
        public UnityEvent<CellEventArgs> DeletionCellClicked = new();

        private readonly UnitPlacementTransactionLogic unitPlacementTransaction;
        private readonly UnitDeletionTransactionLogic unitDeletionTransaction;
        private readonly int factionId;

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


        public CellsManager([NotNull] Transform cellsParent, [NotNull] CellUnitSpawner unitSpawner,
            [NotNull] ActiveTaskContainer activeTask, int factionId)
        {
            this.factionId = factionId;
            this.cellsParent = cellsParent ?? throw new ArgumentNullException(nameof(cellsParent));
            this.unitSpawner = unitSpawner ?? throw new ArgumentNullException(nameof(unitSpawner));
            this.activeTask = activeTask ?? throw new ArgumentNullException(nameof(activeTask));
            incomeManager = EconomySystem.Instance.FactionsEconomiesDictionary[factionId].IncomeManager;
            unitSpawner.OnUnitsSpawned.AddListener(OnCellUnitSpawned);
            
            unitPlacementTransaction = new UnitPlacementTransactionLogic(factionId);
            unitDeletionTransaction = new UnitDeletionTransactionLogic(factionId);
        }

        public Dictionary<int, UnitCell> UnitCells => unitCells;

        public void OnEnabled(DeleteButton deleteButton)
        {
            PrepareCells();

            foreach (var (_, unitCell) in UnitCells)
            {
                unitCell.CellAdditiveEntered.AddListener(OnCellAdditiveEntered);
                unitCell.CellDeletionEntered.AddListener(OnCellDeletionEntered);
                unitCell.CellExit.AddListener(OnCellExit);
                unitCell.CellAdditiveClicked.AddListener(OnCellAdditiveClicked);
                unitCell.CellDeletionClicked.AddListener(OnCellDeletionClicked);
                unitCell.DeleteButton = deleteButton;
            }

            if (unitSpawner)
                unitSpawner.OnUnitsSpawned.AddListener(OnCellUnitSpawned);
        }

        private void OnCellAdditiveEntered(CellEventArgs arg0)
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
            //if (cellGroupIds.TryGetValue(arg0.CellId, out var cellGroup))
            //{
            //    if (unitCellsGroups.TryGetValue(cellGroup, out var cellIds))
            //    {
            //        foreach (var cellId in cellIds)
            //            unitCells[cellId].OnCellExit();
            //        return;
            //    }
            //}
            //unitCells[arg0.CellId].OnCellExit();
        }

        public void OnCellAdditiveClicked(CellEventArgs arg0)
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

            AdditiveCellClicked?.Invoke(arg0);
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

            UnitCells[groupId].CreateDecoObject(
                activeTask.UnitCreationTask.TargetObject,
                activeTask.UnitCreationTask.SpawnParticleSystem,
                activeTask.UnitCreationTask.DeletionParticleSystem,
                averagePosition / taskPopulation,
                arg0.UnitScaleFactor);
            var incomeSourceId = incomeManager.AddSource(
                unitPlacementCosts.WarScrap * unitPlacementCosts.IncomeRatio,
                unitPlacementCosts.IncomePaymentInterval);
            unitIncomeSources.Add(groupId, incomeSourceId);
            positionedUnitsPrefabs.Add(groupId, unitToSpawn);
        }

        private void OnCellDeletionClicked(CellEventArgs arg0)
        {
            if (!cellGroupIds.TryGetValue(arg0.CellId, out var cellGroup)) return;
            if (!unitCellsGroups.TryGetValue(cellGroup, out var cellIds))
            {
                Debug.LogError($"[CellManager] The unitCellsGroups did not sync with cellGroupIds");
                return;
            }


            var unitToDelete = positionedUnitsPrefabs[cellGroup];
            var unitPlacementCosts = unitToDelete.GetComponent<UnitPlacementCosts>();
            if (unitPlacementCosts)
                if (!unitDeletionTransaction.Process(unitPlacementCosts)) return;

            incomeManager.RemoveSource(unitIncomeSources[cellGroup]);
            unitIncomeSources.Remove(cellGroup);

            DeletionCellClicked?.Invoke(arg0);
            foreach (var cellId in cellIds)
            {
                UnitCells[cellId].OnCellDeletionClicked();
                unitSpawner.RemoveUnit(cellId);
                cellGroupIds.Remove(cellId);
            }

            unitCellsGroups.Remove(cellGroup);
            positionedUnitsPrefabs.Remove(cellGroup);
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

        public void OnDisabled()
        {
            foreach (var (_, unitCell) in UnitCells)
                unitCell.CellAdditiveClicked.RemoveListener(OnCellAdditiveClicked);

            if (unitSpawner)
                unitSpawner.OnUnitsSpawned.RemoveListener(OnCellUnitSpawned);
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
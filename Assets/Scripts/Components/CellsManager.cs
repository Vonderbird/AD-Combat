using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using RTSEngine.Entities;
using RTSEngine.Event;
using UnityEngine;
using UnityEngine.Events;

public class CellsManager
{
    private Transform cellsParent;
    private readonly CellUnitSpawner unitSpawner;
    private readonly ActiveTaskContainer activeTask;
    private readonly Dictionary<int, UnitCell> unitCells = new();
    private readonly Dictionary<int, Vector3> cellPositions = new();

    private Dictionary<int, List<int>> unitCellsGroups = new();
    private readonly Dictionary<int, int> cellGroupIds = new();

    public UnityEvent<CellEventArgs> AdditiveCellClicked = new();
    public UnityEvent<CellEventArgs> DeletionCellClicked = new();

    public CellsManager([NotNull] Transform cellsParent, [NotNull] CellUnitSpawner unitSpawner,
        [NotNull] ActiveTaskContainer activeTask)
    {
        this.cellsParent = cellsParent ?? throw new ArgumentNullException(nameof(cellsParent));
        this.unitSpawner = unitSpawner ?? throw new ArgumentNullException(nameof(unitSpawner));
        this.activeTask = activeTask ?? throw new ArgumentNullException(nameof(activeTask));
        unitSpawner.OnUnitsSpawned.AddListener(OnResetCell);
    }

    public void OnEnabled(DeleteButton deleteButton)
    {
        PrepareCells();

        foreach (var (_, unitCell) in unitCells)
        {
            unitCell.CellAdditiveEntered.AddListener(OnCellAdditiveEntered);
            unitCell.CellDeletionEntered.AddListener(OnCellDeletionEntered);
            unitCell.CellExit.AddListener(OnCellExit);
            unitCell.CellAdditiveClicked.AddListener(OnCellAdditiveClicked);
            unitCell.CellDeletionClicked.AddListener(OnCellDeletionClicked);
            unitCell.DeleteButton = deleteButton;
        }

        if (unitSpawner)
            unitSpawner.OnUnitsSpawned.AddListener(OnResetCell);
    }

    private void OnCellAdditiveEntered(CellEventArgs arg0)
    {
        if (!activeTask.HasValue) return;

        var taskPopulation = 0;
        foreach (var requiredResource in activeTask.UnitCreationTask.RequiredResources)
        {
            if (requiredResource.type.name == "population")
            {
                taskPopulation = requiredResource.value.amount;
            }
        }
        var nearestCellIds = cellPositions.OrderBy(p => Vector3.Distance(arg0.HitPoint, p.Value)).Take(taskPopulation).Select(p=>p.Key).ToList();
        foreach (var cellId in nearestCellIds)
            unitCells[cellId].OnCellAdditiveEntered();
    }

    private void OnCellDeletionEntered(CellEventArgs arg0)
    {
        if (!cellGroupIds.TryGetValue(arg0.CellId, out var cellGroup)) return;
        if (!unitCellsGroups.TryGetValue(cellGroup, out var cellIds))
        {
            Debug.LogError($"[CellManager] The unitCellsGroups did not sync with cellGroupIds");
            return;
        }

        foreach (var cellId in cellIds)
            unitCells[cellId].OnCellDeletionEntered();
    }

    private void OnCellExit(CellEventArgs arg0)
    {
        foreach (var (_,unitCell) in unitCells)
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
        if (!activeTask.HasValue) return;
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
        var nearestCellIds = cellPositions.OrderBy(p => Vector3.Distance(arg0.HitPoint, p.Value)).Take(taskPopulation).Select(p => p.Key).ToList();

        AdditiveCellClicked?.Invoke(arg0);
        unitCellsGroups.Add(arg0.CellId, new List<int>(taskPopulation));
        foreach (var cellId in nearestCellIds)
        {
            unitCells[cellId].OnCellAdditiveClicked(activeTask.UnitCreationTask.TargetObject);
            unitCellsGroups[arg0.CellId].Add(cellId);
            cellGroupIds.Add(cellId, arg0.CellId);
        }
    }

    private void OnCellDeletionClicked(CellEventArgs arg0)
    {
        if (!cellGroupIds.TryGetValue(arg0.CellId, out var cellGroup)) return;
        if (!unitCellsGroups.TryGetValue(cellGroup, out var cellIds))
        {
            Debug.LogError($"[CellManager] The unitCellsGroups did not sync with cellGroupIds");
            return;
        }

        DeletionCellClicked?.Invoke(arg0);
        foreach (var cellId in cellIds)
        {
            unitCells[cellId].OnCellDeletionClicked();
            unitSpawner.RemoveUnit(cellId);
            cellGroupIds.Remove(arg0.CellId);
        }
        unitCellsGroups.Remove(cellGroup);
    }

    public void OnDisabled()
    {
        foreach (var (_, unitCell) in unitCells)
            unitCell.CellAdditiveClicked.RemoveListener(OnCellAdditiveClicked);

        if (unitSpawner)
            unitSpawner.OnUnitsSpawned.RemoveListener(OnResetCell);
    }

    public void PrepareCells()
    {
        if (unitCells.Count > 0) return;

        var unitCellsArray = cellsParent.GetComponentsInChildren<UnitCell>();
        foreach (var unitCell in unitCellsArray)
        {
            unitCells.Add(unitCell.CellId, unitCell);
            cellPositions.Add(unitCell.CellId, unitCell.transform.position);
        }
    }

    public void OnEntitySelected(IEntity sender, EntitySelectionEventArgs args)
    {
        foreach (var (_, unitCell) in unitCells)
        {
            unitCell.IsBuildingSelected = true;
        }
    }

    public void OnEntityDeselected(IEntity sender, EntityDeselectionEventArgs args)
    {
        foreach (var (_, unitCell) in unitCells)
        {
            unitCell.IsBuildingSelected = false;
        }
    }


    public void OnResetCell(UnitsSpawnEventArgs spawnEventArgs)
    {
        foreach (var unitId in spawnEventArgs.UnitIds)
        {
            unitCells[unitId].ResetCell();
            //SpawnUnitRemoved?.Invoke();
        }
    }
}
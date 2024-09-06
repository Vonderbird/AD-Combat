using RTSEngine;
using RTSEngine.EntityComponent;
using RTSEngine.UI;
using System.Collections.Generic;
using RTSEngine.Entities;
using RTSEngine.Event;
using RTSEngine.Selection;
using RTSEngine.Task;
using UnityEngine;
using UnityEngine.Events;
using IUnit = RTSEngine.Entities.IUnit;
using RTSEngine.UnitExtension;

public class CellFillerComponent : PendingTaskEntityComponentBase, IUnitCreator, IEntityPostInitializable
{
    [SerializeField] private Transform cellsParent;
    private Dictionary<int, UnitCell> unitCells;

    //[SerializeField]
    //private List<UnitCreationTask> taskUI = null;

    //[SerializeField]
    //private EntityComponentTaskUIAsset taskUI = null;

    //[SerializeField] private UnitSpawnData taskData;

    [SerializeField, Tooltip("List of unit creation tasks that can be launched through this component.")]
    private List<UnitCreationTask> creationTasks = new List<UnitCreationTask>();
    private List<UnitCreationTask> allCreationTasks;

    private UnitCreationTask activeTaskData;

    [SerializeField]
    private Transform spawnTransform = null;

    [SerializeField]
    private Transform gotoTransform = null;

    private CellUnitSpawner unitSpawner;

    //[SerializeField, EnforceType(typeof(IUnit), sameScene: false, prefabOnly: true)]
    //private GameObject activePrefabObj;

    //private IUnit activeUnit;

    public UnityEvent<IUnit> SpawnUnitAdded = new();
    //public UnityEvent<IUnit> SpawnUnitRemoved = new();

    public override IReadOnlyList<IEntityComponentTaskInput> Tasks { get; }

    protected IUnitManager unitMgr { private set; get; }

    #region Initializing/Terminating
    protected override void OnPendingInit()
    {
        if (!spawnTransform.IsValid() || !gotoTransform.IsValid() || !cellsParent.IsValid())// || !unitPrefabObj.IsValid())
        {
            Debug.LogError("[CellFillerComponent] All inspector fields must be assigned!");
            return;
        }

        if (!Entity.IsFactionEntity())
        {
            Debug.LogError($"[CellFillerComponent] This component can only be attached to unit or building entities!", gameObject);
            return;
        }

        //activeUnit = activePrefabObj.GetComponent<IUnit>();
        this.unitMgr = gameMgr.GetService<IUnitManager>();
        unitSpawner = gameMgr.GetService<CellUnitSpawner>();
        unitSpawner.OnUnitsSpawned.AddListener(OnResetCell);
        Entity.Selection.Selected += OnEntitySelected;
        Entity.Selection.Deselected += OnEntityDeselected;
        activeTaskData = creationTasks[0];
        Entity.Selection.OnSelected(
            new EntitySelectionEventArgs(SelectionType.single, SelectedType.newlySelected, true));


        // Initialize creation tasks
        allCreationTasks = new List<UnitCreationTask>();
        int taskID = 0;
        for (taskID = 0; taskID < creationTasks.Count; taskID++)
        {
            creationTasks[taskID].Init(this, taskID, gameMgr);
            creationTasks[taskID].Enable();
        }
        allCreationTasks.AddRange(creationTasks);

        Debug.Log($"a: {gameMgr.GetFactionSlot(0).Data.name}, {gameMgr.GetFactionSlot(0).Data.color}");
        //unitCreationTask.Init(this, 0, gameMgr);
        //unitCreationTask.Enable();
    }


    private int activeTaskId = 0;
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            activeTaskId = (activeTaskId + 1) % creationTasks.Count;
            OnTaskUIClick(activeTaskId);
        }
    }
    
    #endregion

    private void OnEntitySelected(IEntity sender, EntitySelectionEventArgs args)
    {
        foreach (var (_, unitCell) in unitCells)
            unitCell.IsBuildingSelected = true;
    }

    private void OnEntityDeselected(IEntity sender, EntityDeselectionEventArgs args)
    {
        foreach (var (_, unitCell) in unitCells)
            unitCell.IsBuildingSelected = false;
    }

    //protected override bool OnTaskUICacheUpdate(List<EntityComponentTaskUIAttributes> taskUIAttributesCache, List<string> disabledTaskCodesCache)
    //{
    //    return RTSHelper.OnSingleTaskUIRequest(
    //        this,
    //        taskUIAttributesCache,
    //        disabledTaskCodesCache,
    //        taskData.TaskUi,
    //        enforceCanLaunchTask: false);
    //}

    #region Handling UnitCreation Actions
    protected override ErrorMessage CompleteTaskActionLocal(int creationTaskID, bool playerCommand)
    {
        Debug.Log("Task Completed!!!");
        UnitCreationTask nextTask = allCreationTasks[creationTaskID];

        unitMgr.CreateUnit(
            nextTask.TargetObject,
            SpawnPosition,
            Quaternion.identity,
            new InitUnitParameters
            {
                factionID = Entity.FactionID,
                free = false,

                setInitialHealth = false,

                giveInitResources = true,

                rallypoint = factionEntity.Rallypoint,
                creatorEntityComponent = this,

                useGotoPosition = true,
                gotoPosition = SpawnPosition,

                isSquad = nextTask.SquadData.enabled,
                squadCount = nextTask.SquadData.count,

                playerCommand = playerCommand
            });

        return ErrorMessage.none;

    }
    #endregion
    


    #region Unit Creator Specific Methods
    // Find the task ID that allows to create the unit in the parameter
    public int FindTaskIndex(string unitCode)
    {
        return creationTasks.FindIndex(task => task.TargetObject.Code == unitCode);
    }
    #endregion

    #region Task UI
    protected override string GetTaskTooltipText(IEntityComponentTaskInput taskInput)
    {
        UnitCreationTask nextTask = taskInput as UnitCreationTask;

        textDisplayer.UnitCreationTaskTooltipToString(
            nextTask,
            out string tooltipText);

        return tooltipText;
    }
    #endregion

    public void OnTaskUIClick(int taskId) // ???????????????
    {
        if (creationTasks[taskId].IsValid())
        {
            // activeUnit = ...
            activeTaskData = creationTasks[taskId];
            Debug.Log($"activeTaskData is {activeTaskData.TargetObject.Name}");
        }
    }

    //public override bool OnTaskUIClick(EntityComponentTaskUIAttributes taskAttributes) // ???????????????
    //{
    //    Debug.Log($"taskData.TaskUi.Key: {creationTasks[0].TargetObject.Code}");
    //    for (int i = 0; i < creationTasks.Count; i++)
    //    {
    //        if (creationTasks[i].IsValid() && taskAttributes.data.code == creationTasks[i].TargetObject.Code)
    //        {
    //            // activeUnit = ...
    //            Debug.Log("Select Main Unit");
    //            activeTaskData = creationTasks[i];
    //            return true;
    //        }
    //    }

    //    return false;
    //}

    //public void SetActiveUnitPrefab(IUnit unitPrefab)
    //{
    //    activeUnit = unitPrefab;
    //}
    
    void OnEnable()
    {
        PrepareCells();
        foreach (var (_, unitCell) in unitCells)
        {
            unitCell.CellClicked.AddListener(OnCellClicked);
            unitCell.CellRightClicked.AddListener(OnCellRightClicked);
        }
        if(unitSpawner)
            unitSpawner.OnUnitsSpawned.AddListener(OnResetCell);
    }


    void OnDisable()
    {
        foreach (var (_, unitCell) in unitCells)
            unitCell.CellClicked.RemoveListener(OnCellClicked);
        if (unitSpawner)
            unitSpawner.OnUnitsSpawned.RemoveListener(OnResetCell);
    }

    private void PrepareCells()
    {
        if (unitCells != null) return;
        unitCells = new Dictionary<int, UnitCell>();
        foreach (var unitCell in cellsParent.GetComponentsInChildren<UnitCell>())
        {
            unitCells.Add(unitCell.CellId, unitCell);
        }
    }

    private void OnCellClicked(CellClickedEventArgs e)
    {
        if (e.DecoObject) return;
        var unitToSpawn = activeTaskData.TargetObject;
        unitSpawner.AddNewUnit(new UnitParameters
        {
            CreatorComponent = this,
            UnitTask = activeTaskData,
            GotoTransform = gotoTransform,
            SpawnTransform = spawnTransform,
            Unit = unitToSpawn,
            Id = e.CellId
        });
        unitCells[e.CellId].CreateDecoObject(unitToSpawn);
        SpawnUnitAdded?.Invoke(unitToSpawn);
    }

    private void OnCellRightClicked(int unitId)
    {
        unitSpawner.RemoveUnit(unitId);
        unitCells[unitId].ResetCell();
    }
    private void OnResetCell(UnitsSpawnEventArgs spawnEventArgs)
    {
        foreach (var unitId in spawnEventArgs.UnitIds)
        {
            unitCells[unitId].ResetCell();
            //SpawnUnitRemoved?.Invoke();
        }
    }

    public Vector3 SpawnPosition { get; }


}

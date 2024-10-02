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


public class UnitPlacementTransaction : ICurrencyDrainer, ICurrencyProducer
{
    private readonly int factionId;

    public UnitPlacementTransaction(int factionID)
    {
        factionId = factionID;
    }
    public bool Pay(decimal value)
    {
        if (!Drain(new WarScrap(value))) return false;
        if (!Produce(new Biofuel(value)))
        {
            if (Produce(new WarScrap(value)))
            {
                return false;
            }
            else
            {

            }
        }
        return true;
    }

    public bool Drain(Biofuel amount)
    {
        throw new System.NotImplementedException();
    }

    public bool Drain(WarScrap amount)
    {
        throw new System.NotImplementedException();
    }

    public bool Produce(Biofuel amount)
    {
        throw new System.NotImplementedException();
    }

    public bool Produce(WarScrap amount)
    {
        throw new System.NotImplementedException();
    }
}

public class CellFillerComponent : PendingTaskEntityComponentBase, IUnitCreator, IEntityPostInitializable


{
    [SerializeField] private Transform cellsParent;

    //[SerializeField]
    //private List<UnitCreationTask> taskUI = null;

    //[SerializeField]
    //private EntityComponentTaskUIAsset taskUI = null;

    //[SerializeField] private UnitSpawnData taskData;

    [SerializeField, Tooltip("List of unit creation tasks that can be launched through this component.")]
    private List<UnitCreationTask> creationTasks = new List<UnitCreationTask>();
    private List<UnitCreationTask> allCreationTasks;
    private ActiveTaskContainer activeTaskData = new();

    [SerializeField]
    private Transform spawnTransform = null;
    private Transform testTransform;

    [SerializeField]
    private Transform gotoTransform = null;

    private CellUnitSpawner unitSpawner;

    public UnityEvent<IUnit> SpawnUnitAdded = new();

    public override IReadOnlyList<IEntityComponentTaskInput> Tasks { get; }

    private SpawnUnitsList spawnUnitsList;

    protected IUnitManager unitMgr { private set; get; }

    private CellsManager cellsManager;

    private DeleteButton deleteButton;
    protected FactionEconomy factionEconomy { private set; get; }

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

        unitSpawner = gameMgr.GetService<CellUnitSpawner>();
        if (cellsManager == null)
        {
            cellsManager = new CellsManager(cellsParent, unitSpawner, activeTaskData);
            deleteButton ??= FindAnyObjectByType<DeleteButton>();
            cellsManager.OnEnabled(deleteButton);
            cellsManager.AdditiveCellClicked.AddListener(OnAdditionCellClicked);
        }
        this.unitMgr = gameMgr.GetService<IUnitManager>
            ();
        Entity.Selection.Selected += cellsManager.OnEntitySelected;
        Entity.Selection.Deselected += cellsManager.OnEntityDeselected;
        Entity.Selection.OnSelected(
            new EntitySelectionEventArgs(SelectionType.single, SelectedType.newlySelected, true));

        spawnUnitsList = FindAnyObjectByType<SpawnUnitsList>();
        if (!spawnUnitsList)
        {
            Debug.LogError($"[CellFillerComponent] Cannot find SpawnUnitsList in the scene");
            return;
        }

        allCreationTasks = new List<UnitCreationTask>();
        int taskID = 0;
        for (taskID = 0; taskID < creationTasks.Count; taskID++)
        {
            creationTasks[taskID].Init(this, taskID, gameMgr);
            creationTasks[taskID].Enable();
            spawnUnitsList.AddSpawnUnitUITask(creationTasks[taskID], OnActivateTask);
        }
        allCreationTasks.AddRange(creationTasks);
        testTransform = new GameObject("Test Transform").transform;
        testTransform.SetParent(spawnTransform);
        factionEconomy = EconomySystem.Instance.FactionsEconomiesDictionary[Entity.FactionID];
    }


    void OnEnable()
    {
        deleteButton ??= FindAnyObjectByType<DeleteButton>();
        if (deleteButton != null)
            deleteButton.Clicked.AddListener(DeactivateTask);

        if (cellsManager != null)
        {
            cellsManager.OnEnabled(deleteButton);
            cellsManager.AdditiveCellClicked.AddListener(OnAdditionCellClicked);
        }
    }

    void OnDisable()
    {
        if (cellsManager != null)
        {
            cellsManager?.OnDisabled();
            cellsManager.AdditiveCellClicked.RemoveListener(OnAdditionCellClicked);
        }
        if (deleteButton)
            deleteButton.Clicked.RemoveListener(DeactivateTask);
    }

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

    public int FindTaskIndex(string unitCode)
    {
        return creationTasks.FindIndex(task => task.TargetObject.Code == unitCode);
    }

    protected override string GetTaskTooltipText(IEntityComponentTaskInput taskInput)
    {
        UnitCreationTask nextTask = taskInput as UnitCreationTask;

        textDisplayer.UnitCreationTaskTooltipToString(
            nextTask,
            out string tooltipText);

        return tooltipText;
    }

    public void OnActivateTask(int taskId) // ???????????????
    {
        if (creationTasks[taskId].IsValid())
            OnActivateTask(creationTasks[taskId]);
    }

    /// <summary>
    /// //////////////////////
    /// </summary>
    /// <param name="e"></param>
    public void OnAdditionCellClicked(CellEventArgs e)
    {
        if (e.IsFilled || !activeTaskData.HasValue) return;
        if (!Drain(new WarScrap(activeTaskData.UnitCreationTask.UnitWarScrapCost))) return;
        if (!Produce(new Biofuel(activeTaskData.UnitCreationTask.UnitWarScrapCost)))
            Debug.LogError($"[CellFillerComponent] BioFuel deposit didn't work!");

        var unitToSpawn = activeTaskData.UnitCreationTask.TargetObject;

        testTransform.localPosition = cellsManager.UnitCells[e.CellId].transform.localPosition;

        unitSpawner.AddNewUnit(new UnitParameters
        {
            CreatorComponent = this,
            UnitTask = activeTaskData.UnitCreationTask,
            GotoTransform = gotoTransform,
            SpawnPosition = testTransform.position,
            Unit = unitToSpawn,
            Id = e.CellId,
            UnitScaleFactor = e.UnitScaleFactor
        });
        SpawnUnitAdded?.Invoke(unitToSpawn);
    }

    public void OnActivateTask(UnitCreationTask task)
    {
        //Debug.Log($"activeTaskData: {activeTaskData}");
        if (task.IsValid())
            activeTaskData.UnitCreationTask = task;
    }

    public void DeactivateTask()
    {
        activeTaskData.UnitCreationTask = null;
    }


    public Vector3 SpawnPosition { get; }


    public UnityEvent<Biofuel> BiofuelDrained { get; }
    public UnityEvent<WarScrap> WarScrapDrained { get; }
    public bool Drain(Biofuel amount)
    {
        return factionEconomy.Withdraw(amount);
    }

    public bool Drain(WarScrap amount)
    {
        return factionEconomy.Withdraw(amount);
    }

    public bool Produce(Biofuel amount)
    {
        return factionEconomy.Deposit(amount);
    }

    public bool Produce(WarScrap amount)
    {
        return factionEconomy.Deposit(amount);
    }
}

using System;
using RTSEngine;
using UnityEngine;
using ADC.Currencies;
using RTSEngine.Event;
using UnityEngine.Events;
using RTSEngine.Entities;
using RTSEngine.Selection;
using RTSEngine.UnitExtension;
using RTSEngine.EntityComponent;
using System.Collections.Generic;
using ADC.API;
using IUnit = RTSEngine.Entities.IUnit;
using SelectionType = RTSEngine.Selection.SelectionType;

namespace ADC.UnitCreation
{

    public class CellFillerComponent : PendingTaskEntityComponentBase, IUnitCreator, IDeactivable


    {
        [SerializeField] private Transform cellsParent;
        [SerializeField] private SpawnPointsManager spawnPointsManager;

        /// <summary>
        /// Group Ids, separate multiple group id with ';'
        /// </summary>
        [SerializeField] private string groupIds;

        /// <summary>
        /// Ids of groups to deactivate on activation of this button, separate multiple group id with ';'
        /// </summary>
        [SerializeField] private string groupIdsToDeactivate;

        public string[] GroupIds { get; set; }
        private string[] GroupIdsToDeactivate { get; set; }

        //[SerializeField]
        //private List<UnitCreationTask> taskUI = null;

        //[SerializeField]
        //private EntityComponentTaskUIAsset taskUI = null;

        //[SerializeField] private UnitSpawnData taskData;

        [SerializeField, Tooltip("List of unit creation tasks that can be launched through this component.")]
        private List<UnitCreationTask> creationTasks = new();

        private List<UnitCreationTask> allCreationTasks;
        private ActiveTaskContainer activeTaskData = new();

        [SerializeField] private Transform spawnTransform = null;
        private Transform unitSpawnPosTestTransform;
        private Transform cellRelativePosTestTransform;

        [SerializeField] private Transform gotoTransform = null;

        private CellUnitSpawner unitSpawner;

        public UnityEvent<IUnit> SpawnUnitAdded = new();

        public EventHandler<SelectionEventArgs> UnitCellSelected;
        public EventHandler<DeselectionEventArgs> UnitCellDeselected;

        public override IReadOnlyList<IEntityComponentTaskInput> Tasks { get; }

        private SpawnUnitsList spawnUnitsList;

        protected IUnitManager unitMgr { private set; get; }

        private CellsManager cellsManager;
        public CellsManager CellsManager => cellsManager;
        public Vector3 SpawnPosition { get; }



        private DeleteButton deleteButton;
        protected IncomeManager incomeManager { private set; get; }
        private UnitPlacementTransactionLogic unitPlacementTransaction;

        private void Awake()
        {
            GroupIds = groupIds.Split(';');
            GroupIdsToDeactivate = groupIdsToDeactivate.Split(';');
            AddToManager();
        }

        protected override void OnPendingInit()
        {
            if (!spawnTransform.IsValid() || !gotoTransform.IsValid() ||
                !cellsParent.IsValid()) // || !unitPrefabObj.IsValid())
            {
                Debug.LogError("[CellFillerComponent] All inspector fields must be assigned!");
                return;
            }

            if (!Entity.IsFactionEntity())
            {
                Debug.LogError(
                    $"[CellFillerComponent] This component can only be attached to unit or building entities!",
                    gameObject);
                return;
            }

            unitSpawner = gameMgr.GetService<CellUnitSpawner>();
            spawnPointsManager.OnInit(this);
            incomeManager = EconomySystem.Instance.FactionsEconomiesDictionary[Entity.FactionID].IncomeManager;
            if (CellsManager == null)
            {
                cellsManager = new CellsManager(cellsParent, unitSpawner, activeTaskData, Entity.FactionID);
                deleteButton ??= FindAnyObjectByType<DeleteButton>();
                CellsManager.OnEnabled(deleteButton);
                CellsManager.AdditiveCellClicked.AddListener(OnAdditionCellClicked);
                //cellsManager.SelectionCellClicked.AddListener(OnSelectionCellClicked);
                CellsManager.UnitCellSelected += UnitCellSelected;
                CellsManager.UnitCellDeselected += UnitCellDeselected;
            }

            this.unitMgr = gameMgr.GetService<RTSEngine.UnitExtension.IUnitManager>
                ();
            Entity.Selection.Selected += CellsManager.OnEntitySelected;
            Entity.Selection.Deselected += CellsManager.OnEntityDeselected;
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
                var targetUnit = creationTasks[taskID].TargetObject;
                if (targetUnit==null)
                {
                    Debug.LogError($"[CellFillerComponent] unit object for the unit creation task {taskID} is not assigned!");
                    return;
                }

                var unitPlacementCosts = targetUnit.GetComponent<UnitPlacementCosts>();
                creationTasks[taskID].Init(this, taskID, gameMgr);
                creationTasks[taskID].Enable();
                if (unitPlacementCosts == null)
                {
                    Debug.LogError(targetUnit.Name +": " + targetUnit.transform.parent);
                    continue;
                }
                spawnUnitsList.AddSpawnUnitUITask(creationTasks[taskID], OnActivateTask, OnDeactivateTask, (float)unitPlacementCosts.WarScrap.Value);
            }

            allCreationTasks.AddRange(creationTasks);
            cellRelativePosTestTransform = new GameObject("Test Transform").transform;
            unitSpawnPosTestTransform = new GameObject("Test Transform").transform;
            cellRelativePosTestTransform.SetParent(cellsParent);
            unitSpawnPosTestTransform.SetParent(spawnTransform);
            unitPlacementTransaction = new UnitPlacementTransactionLogic(Entity.FactionID);
        }


        void OnEnable()
        {
            deleteButton ??= FindAnyObjectByType<DeleteButton>();
            if (deleteButton != null)
                deleteButton.Clicked.AddListener(OnDeactivateTask);

            if (CellsManager != null)
            {
                CellsManager.OnEnabled(deleteButton);
                CellsManager.AdditiveCellClicked.AddListener(OnAdditionCellClicked);
            }
        }

        void OnDisable()
        {
            if (CellsManager != null)
            {
                CellsManager?.OnDisabled();
                CellsManager.AdditiveCellClicked.RemoveListener(OnAdditionCellClicked);
            }

            if (deleteButton)
                deleteButton.Clicked.RemoveListener(OnDeactivateTask);
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
            if (e.IsFilled || !activeTaskData.HasValue || !CellsManager.CellGroupIds.ContainsKey(e.CellId)) return;
            var unitToSpawn = CellsManager.GroupUnit[CellsManager.CellGroupIds[e.CellId]].GetComponent<Unit>();//activeTaskData.UnitCreationTask.TargetObject;

            cellRelativePosTestTransform.position =
                CellsManager.UnitsGroupPosition[CellsManager.CellGroupIds[e.CellId]];
            unitSpawnPosTestTransform.localPosition = cellRelativePosTestTransform.localPosition;

            unitSpawner.AddNewUnit(new UnitParameters
            {
                CreatorComponent = this,
                UnitTask = activeTaskData.UnitCreationTask,
                GotoTransform = gotoTransform,
                SpawnPosition = unitSpawnPosTestTransform.position,
                Unit = unitToSpawn,
                Id = e.CellId,
                UnitScaleFactor = e.UnitScaleFactor,
                SpawnPointsManager = spawnPointsManager
            });
            SpawnUnitAdded?.Invoke(unitToSpawn);
        }


        public void OnActivateTask(UnitCreationTask task)
        {
            //Debug.Log($"activeTaskData: {activeTaskData}");
            if (task.IsValid())
                activeTaskData.UnitCreationTask = task;
        }

        public void OnDeactivateTask()
        {
            activeTaskData.UnitCreationTask = null;

        }

        public IUnitBattleManager GetCorrespondingUnitCell(IUnitBattleManager unitBattleManager)
        {
            if (CellsManager == null) return null;
            foreach (var (groupId, cellUnit) in CellsManager.GroupUnit)
            {
                if (cellUnit.GetType() != unitBattleManager.GetType()) continue;
                unitSpawnPosTestTransform.localPosition = CellsManager.UnitCells[groupId].transform.localPosition;
                if ((unitSpawnPosTestTransform.position - unitBattleManager.Transform.localPosition).sqrMagnitude <
                    1.0f)
                {
                    return cellUnit;
                }
            }

            return null;
        }

        public void OnHomeCameraDeactivated()
        {
            Deactivate();
        }

        public void AddToManager()
        {
            foreach (var id in GroupIds)
            {
                DeactivablesManager.Instance.Add(id, this);
            }
        }

        public void Deactivate()
        {
            cellsManager.OnAllCellsUnselect(null);
        }
    }
}
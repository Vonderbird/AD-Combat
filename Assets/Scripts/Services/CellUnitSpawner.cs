using System;
using System.Collections.Generic;
using System.Linq;
using RTSEngine.Determinism;
using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using RTSEngine.Game;
using RTSEngine.UnitExtension;
using UnityEngine;
using UnityEngine.Events;

public class CellUnitSpawner : MonoBehaviour, IPostRunGameService
{
    [SerializeField]
    private float spawnPeriod = 5.0f;

    //[SerializeField, EnforceType(typeof(IUnit), prefabOnly: true)]
    //private GameObject unitPrefabObj = null;
    //private IUnit unitPrefab;
    //[SerializeField]
    //private int spawnAmount = 3;


    protected IUnitManager unitMgr { private set; get; }

    public TimeModifiedTimer UnitsTimer => unitsTimer;

    private TimeModifiedTimer unitsTimer;
    private Dictionary<int, UnitParameters> unitsSpawn = new();

    private UnitsSpawnEventArgs spawnEventArgs = new();
    public UnityEvent<UnitsSpawnEventArgs> OnUnitsSpawned = new();

    public void Init(IGameManager gameMgr)
    {
        //unitPrefab = unitPrefabObj.GetComponent<IUnit>();
        unitMgr = gameMgr.GetService<IUnitManager>();
        unitsTimer = new TimeModifiedTimer(spawnPeriod);
    }

    private void Update()
    {
        if (!UnitsTimer.ModifiedDecrease()) return;
        spawnEventArgs.UnitIds.Clear();
        foreach (var(id, unitParameters) in unitsSpawn)
        {
            //unitMgr.CreateUnit(unitParameters.Unit, unitParameters.SpawnTransform.position, Quaternion.identity, new InitUnitParameters
            //{
            //    factionID = unitParameters.Unit.FactionID,
            //    free = false,
            //    useGotoPosition = true,
            //    gotoPosition = unitParameters.GotoTransform.position,
            //});
            Debug.Log($"unitParameters.Unit.FactionID: {unitParameters.Unit.FactionID}");
            unitMgr.CreateUnit(
                unitParameters.Unit,
                unitParameters.SpawnTransform.position,
                Quaternion.identity,
                new InitUnitParameters
                {
                    factionID = unitParameters.Unit.FactionID,
                    free = false,

                    setInitialHealth = false,

                    giveInitResources = true,

                    rallypoint = unitParameters.Unit.Rallypoint,
                    creatorEntityComponent = unitParameters.CreatorComponent,

                    useGotoPosition = true,
                    gotoPosition = unitParameters.GotoTransform.position,

                    isSquad = unitParameters.UnitTask.SquadData.enabled,
                    squadCount = unitParameters.UnitTask.SquadData.count,

                    playerCommand = true
                });


            spawnEventArgs.UnitIds.Add(id);
        }
        unitsSpawn.Clear();
        UnitsTimer.Reload(spawnPeriod);
        OnUnitsSpawned?.Invoke(spawnEventArgs);
    }

    public void AddNewUnit(UnitParameters unit)
    {
        unitsSpawn.Add(unit.Id, unit);
    }

    public void RemoveUnit(int unitId)
    {
        if (unitsSpawn.ContainsKey(unitId))
            unitsSpawn.Remove(unitId);
    }
}

public struct UnitParameters
{
    public int Id { get; set; }

    public IUnit Unit { get; set; }

    public UnitCreationTask UnitTask { get; set; }
    public IEntityComponent CreatorComponent { get; set; }

    public Transform SpawnTransform { get; set; }

    public Transform GotoTransform { get; set; }
}

public class UnitsSpawnEventArgs : EventArgs
{
    public HashSet<int> UnitIds { get; set; } = new();
}

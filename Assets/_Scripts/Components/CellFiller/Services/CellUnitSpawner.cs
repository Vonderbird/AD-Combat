using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ADC.API;
using ADC.Currencies;
using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using RTSEngine.Game;
using Sisus.Init;
using UnityEngine;
using UnityEngine.Events;


namespace ADC.UnitCreation
{
    public class CellUnitSpawner : MonoBehaviour<IWaveTimer>, IPostRunGameService
    {
        protected RTSEngine.UnitExtension.IUnitManager unitMgr { private set; get; }

        private readonly Dictionary<int, UnitParameters> unitsSpawn = new();

        private readonly UnitsSpawnEventArgs spawnEventArgs = new();
        public UnityEvent<UnitsSpawnEventArgs> OnUnitsSpawned = new();


        private readonly WaitForSeconds _waitForSeconds = new(0.1f);
        private WaitUntil _waitUntil;
        private IWaveTimer _waveTimer;

        protected override void Init(IWaveTimer waveTimer)
        {
            this._waveTimer = waveTimer;
        }
        
        public void Init(IGameManager gameMgr)
        {
            unitMgr = gameMgr.GetService<RTSEngine.UnitExtension.IUnitManager>();
        }

        private void OnEnable()
        {
            StartCoroutine(DelayedEnable());
        }

        private IEnumerator DelayedEnable()
        {
            yield return null;
            _waveTimer.Begin.AddListener(OnStartWave);
        }

        private void OnDisable()
        {
            try
            {
                _waveTimer.Begin.RemoveListener(OnStartWave);
            }
            catch (Exception)
            {
                //Debug.LogError($"[CellUnitSpawner] {e.Message}");
            }
        }

        private void OnStartWave()
        {
            StartCoroutine(SpawnWaves());
        }
        private IEnumerator SpawnWaves()
        {
            
            Debug.Log("SpawnWaves 1");
            foreach (var (id, unitParameters) in unitsSpawn)
            {
                if (!unitParameters.UnitTask.SpawnParticleSystem) continue;
                var particle = Instantiate(unitParameters.UnitTask.SpawnParticleSystem,
                    unitParameters.SpawnPosition, Quaternion.identity);
                particle.transform.localScale =
                    Vector3.one * particle.ScaleFactor * unitParameters.UnitScaleFactor * 0.5f;
                particle.Play();
            }

            Debug.Log("SpawnWaves 2");
            for (var i = 0; i < 5; i++)
                yield return _waitForSeconds;

            Debug.Log("SpawnWaves 3");
            spawnEventArgs.UnitIds.Clear();
            var unitsSpawn2 = unitsSpawn.ToDictionary((kvp)=> kvp.Key, (kvp)=> kvp.Value);
            foreach (var (id, unitParameters) in unitsSpawn2)
            {
                //var spawnPoint = unitParameters.SpawnPointsManager.Point;
                //while (spawnPoint==null)
                //{
                //    yield return waitForSeconds;
                //    spawnPoint = unitParameters.SpawnPointsManager.GetNextPoint();
                //}
                Debug.Log("SpawnWaves 4 ");
                var initParams = new InitUnitParameters
                {
                    factionID = unitParameters.Unit.FactionID,
                    free = false,

                    setInitialHealth = false,

                    giveInitResources = true,

                    rallypoint = unitParameters.SpawnPointsManager.Rallypoint,
                    creatorEntityComponent = unitParameters.CreatorComponent,

                    useGotoPosition = true,
                    gotoPosition = unitParameters.GotoTransform.position,

                    isSquad = unitParameters.UnitTask.SquadData.enabled,
                    squadCount = unitParameters.UnitTask.SquadData.count,

                    playerCommand = false,
                };
                //Debug.Log($"UnitCode: {JsonUtility.ToJson(initParams.ToInput())}");
                Debug.Log("SpawnWaves 5");
                var message = unitMgr.CreateUnit(
                    unitParameters.Unit,
                    unitParameters.SpawnPosition,
                    Quaternion.identity,
                    initParams);
                Debug.Log($"SpawnWaves 6: {message}");
                spawnEventArgs.UnitIds.Add(id);
            }

            OnUnitsSpawned?.Invoke(spawnEventArgs);

            yield return _waitForSeconds;
        }

        public void AddNewUnit(UnitParameters unit)
        {
            unitsSpawn.Add(unit.Id, unit);
            Debug.Log("Added to unit spawner!");
        }

        public void RemoveUnit(int unitId)
        {
            if (unitsSpawn.ContainsKey(unitId))
                unitsSpawn.Remove(unitId);
        }

    }

    public struct UnitParameters
    {
        public SpawnPointsManager SpawnPointsManager { get; set; }
        public int Id { get; set; }

        public IUnit Unit { get; set; }

        public UnitCreationTask UnitTask { get; set; }
        public IEntityComponent CreatorComponent { get; set; }

        public Vector3 SpawnPosition { get; set; }
        public Transform GotoTransform { get; set; }
        public float UnitScaleFactor { get; set; }
    }

    public class UnitsSpawnEventArgs : EventArgs
    {
        public HashSet<int> UnitIds { get; set; } = new();
    }
}
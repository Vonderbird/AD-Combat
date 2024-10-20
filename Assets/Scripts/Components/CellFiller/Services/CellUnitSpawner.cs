using System;
using System.Collections;
using System.Collections.Generic;
using ADC.Currencies;
using RTSEngine.Determinism;
using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using RTSEngine.Game;
using RTSEngine.UnitExtension;
using UnityEngine;
using UnityEngine.Events;

namespace ADC.UnitCreation
{
    public class CellUnitSpawner : MonoBehaviour, IPostRunGameService
    {
        protected IUnitManager unitMgr { private set; get; }

        private readonly Dictionary<int, UnitParameters> unitsSpawn = new();

        private readonly UnitsSpawnEventArgs spawnEventArgs = new();
        public UnityEvent<UnitsSpawnEventArgs> OnUnitsSpawned = new();


        private readonly WaitForSeconds waitForSeconds = new(0.1f);
        private WaitUntil waitUntil;

        public void Init(IGameManager gameMgr)
        {
            unitMgr = gameMgr.GetService<IUnitManager>();
        }

        private void OnEnable()
        {
            StartCoroutine(DelayedEnable());
        }

        private IEnumerator DelayedEnable()
        {
            yield return null;
            EconomySystem.Instance.StartWave.AddListener(OnStartWave);
        }

        private void OnDisable()
        {
            EconomySystem.Instance.StartWave.RemoveListener(OnStartWave);
        }

        private void OnStartWave()
        {
            StartCoroutine(SpawnWaves());
        }
        private IEnumerator SpawnWaves()
        {
            foreach (var (id, unitParameters) in unitsSpawn)
            {
                if (!unitParameters.UnitTask.SpawnParticleSystem) continue;
                var particle = Instantiate(unitParameters.UnitTask.SpawnParticleSystem,
                    unitParameters.SpawnPosition, Quaternion.identity);
                particle.transform.localScale =
                    Vector3.one * particle.ScaleFactor * unitParameters.UnitScaleFactor * 0.5f;
                particle.Play();
            }

            for (var i = 0; i < 5; i++)
                yield return waitForSeconds;

            spawnEventArgs.UnitIds.Clear();
            foreach (var (id, unitParameters) in unitsSpawn)
            {
                unitMgr.CreateUnit(
                    unitParameters.Unit,
                    unitParameters.SpawnPosition,
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

                        playerCommand = false,
                    });

                spawnEventArgs.UnitIds.Add(id);
            }

            OnUnitsSpawned?.Invoke(spawnEventArgs);

            yield return waitForSeconds;
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

        public Vector3 SpawnPosition { get; set; }
        public Transform GotoTransform { get; set; }
        public float UnitScaleFactor { get; set; }
    }

    public class UnitsSpawnEventArgs : EventArgs
    {
        public HashSet<int> UnitIds { get; set; } = new();
    }
}
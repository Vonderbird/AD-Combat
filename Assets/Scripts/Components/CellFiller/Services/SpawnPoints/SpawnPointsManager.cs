using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ADC.UnitCreation;
using RTSEngine.Determinism;
using RTSEngine.EntityComponent;
using UnityEngine;

namespace ADC
{
    [Serializable]
    public class SpawnPointsManager
    {
        private CellFillerComponent fillerComponent;

        //[SerializeField] private SpawnPoint spawnPoint;
        //[SerializeField] private SpawnPoint[] spawnPoints;
        [SerializeField] private Rallypoint rallypoint;
        //private List<int> freePoints;

        //private System.Random rnd = new ();

        //private WaitForSeconds waitForGateRelease = new(4.2f);

        public IRallypoint Rallypoint => rallypoint;

        //public SpawnPoint Point => spawnPoint;

        public void OnInit(CellFillerComponent fillerComponent)
        {
            this.fillerComponent = fillerComponent;
            //freePoints = Enumerable.Range(0, spawnPoints.Length).ToList();
        }

        //public SpawnPoint GetNextPoint()
        //{
        //    if (freePoints.Count == 0) return null;
        //    var randId = rnd.Next(0, freePoints.Count);
        //    var drawPoint = freePoints[randId];
        //    freePoints.RemoveAt(randId);
        //    this.fillerComponent.StartCoroutine(FreeUpGateTimer(drawPoint));
        //    return spawnPoints[drawPoint];
        //}

        //IEnumerator FreeUpGateTimer(int pointToRelease)
        //{
        //    yield return waitForGateRelease;
        //    freePoints.Add(pointToRelease);
        //}
    }
}

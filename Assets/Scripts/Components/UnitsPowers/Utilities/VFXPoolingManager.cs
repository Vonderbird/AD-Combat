using System;
using ADC.API;
using UnityEngine;
using RTSEngine.Determinism;
using System.Collections.Generic;

namespace ADC
{
    public class VFXPoolingManager : Singleton<VFXPoolingManager>
    {

        [SerializeField] private int initialPoolSize = 1; // Default pool size

        private Dictionary<ParticlePlayer, Queue<Tuple<ParticlePlayer, TimeModifiedTimer>>> vfxPools = new ();


        /// <summary>
        /// Spawns a VFX object from the pool or creates a new one if needed.
        /// </summary>
        public void SpawnVFX(ParticlePlayer vfxPrefab, Vector3 position, Quaternion rotation, float lifetime, ParticleArgs args)
        {
            if (vfxPrefab == null)
            {
                Debug.LogError("VFX prefab is null.");
                return;
            }


            if (!vfxPools.ContainsKey(vfxPrefab))
            {
                InitializePool(vfxPrefab, lifetime);
            }

            Tuple<ParticlePlayer, TimeModifiedTimer> vfxTuple;

            // Check if a pooled object is available
            if (vfxPools[vfxPrefab].Count > 0)
            {
                vfxTuple = vfxPools[vfxPrefab].Dequeue();
                vfxTuple.Item2.Reload(lifetime);
            }
            else
            {
                // If no object is available, instantiate a new one
                vfxTuple = new Tuple<ParticlePlayer, TimeModifiedTimer>(Instantiate(vfxPrefab, transform), new TimeModifiedTimer(lifetime));
            }

            vfxTuple.Item1.transform.position = position;
            vfxTuple.Item1.transform.rotation = rotation;
            vfxTuple.Item1.gameObject.SetActive(true);
            vfxTuple.Item1.Initialize(args);

            // Return the object to the pool after its lifetime
            StartCoroutine(ReturnToPoolAfterTime(vfxPrefab, vfxTuple));
        }

        /// <summary>
        /// Returns a VFX object to the pool after its lifetime.
        /// </summary>
        private System.Collections.IEnumerator ReturnToPoolAfterTime(ParticlePlayer vfxPrefab, Tuple<ParticlePlayer, TimeModifiedTimer> vfxTuple)
        {
            vfxTuple.Item1.Play();
            yield return new WaitUntil(vfxTuple.Item2.ModifiedDecrease);
            vfxTuple.Item1.Stop();
            vfxTuple.Item1.gameObject.SetActive(false);
            vfxPools[vfxPrefab].Enqueue(vfxTuple);
        }

        /// <summary>
        /// Initializes a pool for a given VFX prefab.
        /// </summary>
        private void InitializePool(ParticlePlayer vfxPrefab, float lifetime)
        {
            var pool = new Queue<Tuple<ParticlePlayer, TimeModifiedTimer>>();
            for (var i = 0; i < initialPoolSize; i++)
            {
                var vfxTuple = new Tuple<ParticlePlayer, TimeModifiedTimer>(Instantiate(vfxPrefab, transform), new TimeModifiedTimer(lifetime));
                vfxTuple.Item1.gameObject.SetActive(false);
                pool.Enqueue(vfxTuple);
            }
            vfxPools[vfxPrefab] = pool;
        }
    }

}

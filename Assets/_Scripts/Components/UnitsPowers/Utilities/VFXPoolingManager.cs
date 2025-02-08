using ADC.API;
using UnityEngine;
using System.Collections.Generic;
using Sisus.Init;

namespace ADC
{

    [Service]
    public class VFXPoolingManager : MonoBehaviour, IVFXPoolingManager
    {

        [SerializeField] private int initialPoolSize = 1; // Default pool size

        private Dictionary<ParticlePlayer, Queue<ParticlePlayer>> vfxPools = new ();


        /// <summary>
        /// Spawns a VFX object from the pool or creates a new one if needed.
        /// </summary>
        public ParticlePlayer SpawnVfx(ParticlePlayer vfxPrefab, Vector3 position, Quaternion rotation, ParticleArgs args)
        {
            if (vfxPrefab == null)
            {
                Debug.LogError("VFX prefab is null.");
                return null;
            }

            if (!vfxPools.ContainsKey(vfxPrefab))
            {
                InitializePool(vfxPrefab);
            }

            ParticlePlayer vfxPlayer;

            // Check if a pooled object is available
            if (vfxPools[vfxPrefab].Count > 0)
            {
                vfxPlayer = vfxPools[vfxPrefab].Dequeue();
                //vfxTuple.Item2.Reload(lifetime);
            }
            else
            {
                // If no object is available, instantiate a new one
                vfxPlayer =  Instantiate(vfxPrefab, transform);
            }

            vfxPlayer.transform.position = position;
            vfxPlayer.transform.rotation = rotation;
            vfxPlayer.gameObject.SetActive(true);
            vfxPlayer.Initialize(vfxPrefab, args);

            // Return the object to the pool after its lifetime

            vfxPlayer.Play();
            vfxPlayer.Terminated += ReturnToPoolAfterTermination;
            //StartCoroutine(ReturnToPoolAfterTime(vfxPrefab, vfxPlayer));
            return vfxPlayer;
        }


        public ParticlePlayer SpawnVfx(ParticlePlayer vfxPrefab, ParticleArgs args)
        {
            if (vfxPrefab == null)
            {
                Debug.LogError("VFX prefab is null.");
                return null;
            }

            if (!vfxPools.ContainsKey(vfxPrefab))
            {
                InitializePool(vfxPrefab);
            }

            ParticlePlayer vfxPlayer;

            // Check if a pooled object is available
            if (vfxPools[vfxPrefab].Count > 0)
            {
                vfxPlayer = vfxPools[vfxPrefab].Dequeue();
            }
            else
            {
                vfxPlayer = Instantiate(vfxPrefab, transform);
            }

            vfxPlayer.gameObject.SetActive(true);
            vfxPlayer.Initialize(vfxPrefab, args);

            // Return the object to the pool after its lifetime
            vfxPlayer.Terminated += ReturnToPoolAfterTermination;
            //StartCoroutine(ReturnToPoolAfterTime(vfxPrefab, vfxTuple));
            return vfxPlayer;
        }

        private void ReturnToPoolAfterTermination(ParticlePlayer vfxPlayer)
        {
            vfxPlayer.Stop();
            TransformBinderManager.Instance.UnbindTransform(vfxPlayer.transform);
            vfxPlayer.gameObject.SetActive(false);
            vfxPools[vfxPlayer.SourcePrefab].Enqueue(vfxPlayer);
        }


        /// <summary>
        /// Initializes a pool for a given VFX prefab.
        /// </summary>
        private void InitializePool(ParticlePlayer vfxPrefab)
        {
            var pool = new Queue<ParticlePlayer>();
            for (var i = 0; i < initialPoolSize; i++)
            {
                var vfxPlayer = Instantiate(vfxPrefab, transform);
                vfxPlayer.gameObject.SetActive(false);
                pool.Enqueue(vfxPlayer);
            }
            vfxPools[vfxPrefab] = pool;
        }

    }

}

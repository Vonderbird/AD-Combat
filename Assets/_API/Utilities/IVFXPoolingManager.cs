using UnityEngine;

namespace ADC.API
{
    public interface IVFXPoolingManager
    {
        /// <summary>
        /// Spawns a VFX object from the pool or creates a new one if needed.
        /// </summary>
        ParticlePlayer SpawnVfx(ParticlePlayer vfxPrefab, Vector3 position, Quaternion rotation, ParticleArgs args);

        ParticlePlayer SpawnVfx(ParticlePlayer vfxPrefab, ParticleArgs args);
    }
}
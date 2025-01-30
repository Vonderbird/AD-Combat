using UnityEngine;

namespace ADC.API
{
    public struct SkinnedMeshVfxArgs : ParticleArgs
    {
        public float Lifespan { get; set; }
        public SkinnedMeshRenderer SkinnedMesh { get; set; }
    }
}
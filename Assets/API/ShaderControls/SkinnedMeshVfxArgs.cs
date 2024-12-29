using UnityEngine;

namespace ADC.API
{
    public class SkinnedMeshVfxArgs : ParticleArgs
    {
        public float Lifespan { get; set; }
        public SkinnedMeshRenderer SkinnedMesh { get; set; }
    }
}
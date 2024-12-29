using UnityEngine;

namespace ADC.API
{
    public class FollowerVfxArgs : ParticleArgs
    {
        public Transform Transform { get; set; }
        public bool AutoCalculateOffset { get; set; } = true;
        public Vector3 PositionOffset { get; set; } = default;
        public Vector3 RotationOffset { get; set; } = default;
        public Vector3 ScaleOffset { get; set; } = default;

    }
}
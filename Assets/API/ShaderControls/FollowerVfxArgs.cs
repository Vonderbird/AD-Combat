using UnityEngine;

namespace ADC.API
{
    public struct FollowerVfxArgs : ParticleArgs
    {
        public FollowerVfxArgs(Transform transform, bool autoCalculateOffset = true, Vector3 positionOffset = default, Vector3 rotationOffset = default, Vector3 scaleOffset = default)
        {
            AutoCalculateOffset = autoCalculateOffset;
            PositionOffset = positionOffset;
            RotationOffset = rotationOffset;
            ScaleOffset = scaleOffset;
            Transform = transform;
        }

        public Transform Transform { get; set; }
        public bool AutoCalculateOffset { get; set; }
        public Vector3 PositionOffset { get; set; }
        public Vector3 RotationOffset { get; set; }
        public Vector3 ScaleOffset { get; set; }

    }
}
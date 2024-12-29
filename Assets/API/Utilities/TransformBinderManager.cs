using System.Collections.Generic;
using UnityEngine;

public class TransformBinderManager : MonoBehaviour
{
    private static TransformBinderManager _instance;
    public static TransformBinderManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("TransformBinderManager");
                _instance = obj.AddComponent<TransformBinderManager>();
                DontDestroyOnLoad(obj); // Ensure it persists across scenes
            }
            return _instance;
        }
    }

    private readonly Dictionary<Transform, TransformBinding> bindings = new Dictionary<Transform, TransformBinding>();

    private void LateUpdate()
    {
        foreach (var kvp in new List<KeyValuePair<Transform, TransformBinding>>(bindings))
        {
            var binding = kvp.Value;

            // Cleanup if either the source or target has been destroyed
            if (binding.Source == null || binding.Target == null)
            {
                bindings.Remove(kvp.Key);
                continue;
            }

            // Apply the binding properties with offsets
            if (binding.BindPosition)
                binding.Target.position = binding.Source.position + binding.PositionOffset;

            if (binding.BindRotation)
                binding.Target.rotation = binding.Source.rotation * Quaternion.Euler(binding.RotationOffset);

            if (binding.BindScale)
                binding.Target.localScale = Vector3.Scale(binding.Source.localScale, binding.ScaleOffset);
        }
    }

    public void BindTransform(Transform source, Transform target, bool bindPosition = true, bool bindRotation = true, bool bindScale = false,
        Vector3? positionOffset = null, Vector3? rotationOffset = null, Vector3? scaleOffset = null, bool autoCalculateOffsets = true)
    {
        if (source == null || target == null)
        {
            Debug.LogError("Source or Target is null. Cannot bind.");
            return;
        }

        // Avoid duplicate bindings for the same source-target pair
        if (bindings.ContainsKey(target)) return;

        var calculatedPositionOffset = positionOffset ?? Vector3.zero;
        var calculatedRotationOffset = rotationOffset ?? Vector3.zero;
        var calculatedScaleOffset = scaleOffset ?? Vector3.one;

        if (autoCalculateOffsets)
        {
            calculatedPositionOffset = target.position - source.position;
            calculatedRotationOffset = (target.rotation * Quaternion.Inverse(source.rotation)).eulerAngles;
            calculatedScaleOffset = new Vector3(
                target.localScale.x / source.localScale.x,
                target.localScale.y / source.localScale.y,
                target.localScale.z / source.localScale.z
            );
        }

        bindings[target] = new TransformBinding
        {
            Source = source,
            Target = target,
            BindPosition = bindPosition,
            BindRotation = bindRotation,
            BindScale = bindScale,
            PositionOffset = calculatedPositionOffset,
            RotationOffset = calculatedRotationOffset,
            ScaleOffset = calculatedScaleOffset
        };
    }

    public void UnbindTransform(Transform target)
    {
        if (target != null && bindings.ContainsKey(target))
            bindings.Remove(target);
    }

    public void UnbindAll()
    {
        bindings.Clear();
    }

    private class TransformBinding
    {
        public Transform Source;
        public Transform Target;
        public bool BindPosition;
        public bool BindRotation;
        public bool BindScale;
        public Vector3 PositionOffset;
        public Vector3 RotationOffset;
        public Vector3 ScaleOffset;
    }
}

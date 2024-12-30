using ADC.API;
using UnityEngine;
using UnityEngine.VFX;

public class MeleeRedVFXPlayer : ParticlePlayer
{
    [SerializeField] private VisualEffect vfx;
    private Transform defaultParent;

    private void Start()
    {
        defaultParent = transform.parent;
    }

    public override void Initialize(ParticlePlayer sourcePrefab, ParticleArgs args)
    {
        SourcePrefab = sourcePrefab;
        if (args is not FollowerVfxArgs fArgs) return;
        ResetTransform();
        TransformBinderManager.Instance.BindTransform(fArgs.Transform, transform,
            bindScale: true, positionOffset: fArgs.PositionOffset, rotationOffset: fArgs.RotationOffset,
            scaleOffset: fArgs.ScaleOffset, autoCalculateOffsets: fArgs.AutoCalculateOffset);
    }

    public override void Play()
    {
        Debug.Log("Shield Bubble VFX Played");
        vfx.SendEvent("hit");
    }

    public override void Stop()
    {
        //Debug.Log("Shield Bubble VFX Stopped");
        //vfx.SendEvent("end");
        if (SourcePrefab == null) return;
        transform.parent = defaultParent;
        TransformBinderManager.Instance.UnbindTransform(transform);
    }

    public override void Hit()
    {
        vfx.SendEvent("crit");
    }

    private void ResetTransform()
    {
        transform.parent = null;
        transform.position = new Vector3(0, 0, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
    }
}
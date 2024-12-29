using ADC.API;
using UnityEngine;
using UnityEngine.VFX;

public class ShieldBubbleOrangeVFXPlayer : ParticlePlayer
{
    [SerializeField] private VisualEffect vfx;
    //[SerializeField] private VFXPropertyBinder propertyBinder;
    //[SerializeField] private VFXTransformBinder transformBinder;
    [SerializeField] private bool loop = true;
    public bool Loop { get => loop; set => loop = value; }
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
            bindScale: true, positionOffset:fArgs.PositionOffset, rotationOffset:fArgs.RotationOffset, 
            scaleOffset:fArgs.ScaleOffset, autoCalculateOffsets:fArgs.AutoCalculateOffset);
        if (Loop)
            vfx.SendEvent("loop");
    }

    public override void Play()
    {
        Debug.Log("Shield Bubble VFX Played");
        vfx.SendEvent("create");
        if (Loop)
            vfx.SendEvent("loop");
        vfx.SendEvent("hit");
    }

    public override void Stop()
    {
        Debug.Log("Shield Bubble VFX Stopped");
        vfx.SendEvent("end");
        transform.parent = defaultParent;
        TransformBinderManager.Instance.UnbindTransform(transform);
    }

    public override void Hit()
    {
        vfx.SendEvent("hit");
    }

    private void ResetTransform()
    {
        transform.parent = null;
        transform.position = new Vector3(0, 0, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
    }
}
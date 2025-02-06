using ADC.API;
using UnityEngine;
using UnityEngine.VFX;

public class SimpleVfxPlayer : ParticlePlayer
{
    [SerializeField] private VisualEffect vfx;
    //[SerializeField] private VFXPropertyBinder propertyBinder;
    //[SerializeField] private VFXTransformBinder transformBinder;
    private Transform defaultParent;

    private void Start()
    {
        defaultParent = transform.parent;
    }

    public override void Initialize(ParticlePlayer sourcePrefab, ParticleArgs args)
    {
        SourcePrefab = sourcePrefab;
    }

    private void OnEnable()
    {
        vfx.Stop();
    }

    public override void Play()
    {
        vfx.Stop();
        vfx.Play();
    }

    public override void Stop()
    {
        vfx.Stop();
    }

    public override void Hit()
    {
        vfx.Stop();
        vfx.Play();
    }

}
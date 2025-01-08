using System.Collections;
using System.Collections.Generic;
using ADC.API;
using RTSEngine.Determinism;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;


using UnityEngine.VFX;

namespace UnityEngine.VFX.Utility
{
}


public class EnergizeVioletVFXPlayer : ParticlePlayer
{
    [SerializeField] private VisualEffect vfx;
    [SerializeField] private VFXPropertyBinder propertyBinder;
    //[SerializeField] private VFXTransformBinder transformBinder;
    [SerializeField] private bool loop = true;
    private float lifeSpan = 0;
    private TimeModifiedTimer lifeTimer = new();
    public bool Loop { get => loop; set => loop = value; }
    private Transform defaultParent;
    private WaitUntil waitForLife;

    private void Start()
    {
        defaultParent = transform.parent;
        waitForLife = new WaitUntil(lifeTimer.ModifiedDecrease);
    }

    private Coroutine lifeCoroutine;
    public override void Initialize(ParticlePlayer sourcePrefab, ParticleArgs args)
    {

        SourcePrefab = sourcePrefab;
        if (args is not SkinnedMeshVfxArgs eArgs) return;
        lifeSpan = eArgs.Lifespan;
        vfx.SetSkinnedMeshRenderer("SkinnedMesh", eArgs.SkinnedMesh);
        //propertyBinder.ClearPropertyBinders();
        //transformBinder.Target = eArgs.SkinnedMesh.transform;
        //transformBinder.Property = "SkinnedMeshTransform";
        //propertyBinder.m_Bindings.Add(transformBinder);
        if (propertyBinder.m_Bindings[0] is VFXCustomTransformBinder vtb)
        {
            //var targetField = vtb.GetType().GetField("Target", System.Reflection.BindingFlags.Public
            //                                               | System.Reflection.BindingFlags.Instance);
            //targetField.SetValue(vtb, eArgs.SkinnedMesh.transform);

            vtb.Target = eArgs.SkinnedMesh.transform;
        }
        //bind a transform from eArgs using propertyBinder
        //propertyBinder.m_Bindings[0] = new VFXBinderBase().eArgs.SkinnedMesh.transform;

        ResetTransform();
        IsStopped = false;
        if (Loop)
            vfx.SendEvent("loop");
    }

    public override void Play()
    {
        Debug.Log("Hack VFX Playerd");
        if(lifeCoroutine!=null)
            StopCoroutine(lifeCoroutine);
        lifeCoroutine = StartCoroutine(LifeKeeper());
        vfx.SendEvent("create");
        IsStopped = false;
        if (Loop)
            vfx.SendEvent("loop");
    }

    public override void Stop()
    {
        if (IsStopped) return;
        IsStopped = true;
        Debug.Log("Hack VFX Stopped");
        vfx.SendEvent("end");
        transform.parent = defaultParent;

        if (lifeCoroutine != null)
            StopCoroutine(lifeCoroutine);
        Terminated?.Invoke(this);
    }

    public override void Hit()
    {
        IsStopped = false;
        vfx.SendEvent("loop");
    }

    private void ResetTransform()
    {
        transform.parent = null;
        transform.position = new Vector3(0, 0, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
    }

    private IEnumerator LifeKeeper()
    {
        lifeTimer.Reload(lifeSpan);
        yield return waitForLife;
        Terminated?.Invoke(this);
    }
}

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

    public override void Initialize(ParticleArgs args)
    {
        if (args is not SkinnedMeshVfxArgs eArgs) return;
        vfx.SetSkinnedMeshRenderer("SkinnedMesh", eArgs.SkinnedMesh);
        //propertyBinder.ClearPropertyBinders();
        //transformBinder.Target = eArgs.SkinnedMesh.transform;
        //transformBinder.Property = "SkinnedMeshTransform";
        //propertyBinder.m_Bindings.Add(transformBinder);
        //if (propertyBinder.m_Bindings[0] is VFXTransformBinder vtb)
        //    vtb.Target = eArgs.SkinnedMesh.transform;
        //bind a transform from eArgs using propertyBinder
        //propertyBinder.m_Bindings[0] = new VFXBinderBase().eArgs.SkinnedMesh.transform;

        ResetTransform();
        if (Loop)
            vfx.SendEvent("loop");
    }

    public override void Play()
    {
        Debug.Log("Hack VFX Playerd");
        vfx.SendEvent("create");
        if (Loop)
            vfx.SendEvent("loop");
    }

    public override void Stop()
    {
        Debug.Log("Hack VFX Stopped");
        vfx.SendEvent("end");
        transform.parent = defaultParent;
    }

    public void Hit()
    {

    }

    private void ResetTransform()
    {
        transform.parent = null;
        transform.position = new Vector3(0, 0, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
    }
}
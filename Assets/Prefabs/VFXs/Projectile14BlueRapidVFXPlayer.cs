using ADC.API;
using UnityEngine;
using UnityEngine.VFX;

public class Projectile14BlueRapidVFXPlayer : ParticlePlayer
{
    [SerializeField] private ParticleSystem[] projectiles;
    [SerializeField] private ParticleSystem[] hits;
    [SerializeField] private ParticleSystem[] flashes;



    //[SerializeField] private VisualEffect vfx;
    //[SerializeField] private VFXPropertyBinder propertyBinder;
    //[SerializeField] private VFXTransformBinder transformBinder;
    [SerializeField] private bool loop = true;
    public bool Loop { get => loop; set => loop = value; }
    private Transform defaultParent;

    private void Start()
    {
        defaultParent = transform.parent;

        foreach (var projectile in projectiles)
        {
            var main = projectile.main;
            main.loop = loop;
        }
    }

    public override void Initialize(ParticlePlayer sourcePrefab, ParticleArgs args)
    {
        SourcePrefab = sourcePrefab;
        if (args is not FollowerVfxArgs fArgs) return;

        foreach (var projectile in projectiles)
        {
            var main = projectile.main;
            main.loop = loop;
        }
    }

    public override void Play()
    {
        gameObject.SetActive(true);
        foreach (var flash in flashes)
            flash.Play();

        foreach (var projectile in projectiles)
            projectile.Play();
    }

    public override void Stop()
    {
        foreach (var hit in hits)
        {
            hit.Play();
        }

        foreach (var projectile in projectiles)
            projectile.Stop();
    }

    public override void Hit()
    {
        foreach (var hit in hits)
            hit.Play();

        foreach (var flash in flashes)
            flash.Stop();

        foreach (var projectile in projectiles)
            projectile.Stop();
    }

    private void ResetTransform()
    {
        transform.parent = null;
        transform.position = new Vector3(0, 0, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
    }
}
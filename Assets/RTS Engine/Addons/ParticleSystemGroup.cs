using System;
using System.Collections;
using System.Collections.Generic;
using RTSEngine.Event;
using RTSEngine.Utilities;
using UnityEngine;
using UnityEngine.Events;

public class ParticleSystemGroup : MonoBehaviour
{

    //private string code;

    //public string Code
    //{
    //    get
    //    {
    //        if (string.IsNullOrEmpty(code))
    //            code = GUID.Generate().ToString();
    //        return code;
    //    }
    //}
    //public EffectObjectState State { private set; get; }
    private ParticleSystem[] particleSystems;

    [SerializeField] private float scaleFactor = 1.0f;

    public bool DeleteOnStop { get; set; } = true;
    public bool PlayOnAwake { get; set; } = false;
    public float LifeSpan
    {
        get => lifeSpan;
        set => lifeSpan = value;
    }

    public float ScaleFactor
    {
        get => scaleFactor;
        set => scaleFactor = value;
    }

    [SerializeField] private float lifeSpan = -1;

    private Coroutine stopDeletionCoroutine;
    private Coroutine lifeTimeDeletionCoroutine;


    public AudioSource AudioSourceComponent { get; }
    public float CurrLifeTime { get; }

    [SerializeField, Tooltip("Invoked when the effect object is created in the pool.")]
    private UnityEvent initEvent = null;
    [SerializeField, Tooltip("Invoked when the effect object is enabled.")]
    private UnityEvent enableEvent = null;
    [SerializeField, Tooltip("Invoked when the effect object is disabled.")]
    private UnityEvent disableEvent = null;
    [SerializeField, Tooltip("Invoked when the effect object is fully deactivated after it was disabled and the disable time is through.")]
    private UnityEvent deactivateEvent = null;

    // Used as a replacement for parenting the attack object to differnet objects over the course of its lifetime, which can wreck its scale.
    protected FollowTransform followTransform = null;
    public FollowTransform FollowTransform => followTransform;

    protected IGlobalEventPublisher globalEvent { private set; get; }

    private void Awake()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>(includeInactive: false);
        foreach (var system in particleSystems)
        {
            var systemMain = system.main;
            systemMain.playOnAwake = PlayOnAwake;
            if (DeleteOnStop)
                systemMain.loop = false;
        }
    }

    public void Play()
    {
        ParticleSystem[] copyParticles = new List<ParticleSystem>(particleSystems).ToArray();

        foreach (var system in copyParticles)
        {
            if (system)
            {
                system.Clear(true);
                system.Simulate(0.0f, true, true);
                system.Play();
            }
        }


        if (DeleteOnStop)
            stopDeletionCoroutine = StartCoroutine(RunEventOnStop(copyParticles));

        if (LifeSpan > 0)
            lifeTimeDeletionCoroutine = StartCoroutine(RunEventOnLifeEnds());

    }

    private IEnumerator RunEventOnLifeEnds()
    {
        yield return new WaitForSeconds(LifeSpan);

        if (stopDeletionCoroutine != null)
            StopCoroutine(stopDeletionCoroutine);
        Stopped?.Invoke(this, null);
        //Destroy(gameObject);
    }

    private IEnumerator RunEventOnStop(ParticleSystem[] copyParticles)
    {

        while (copyParticles.Length > 0)
        {
            List<int> availableSystemsIndex = new();
            for (var i = 0; i < copyParticles.Length; i++)
            {
                if (!copyParticles[i].isPlaying) continue;
                availableSystemsIndex.Add(i);
            }
            var availableSystems = new ParticleSystem[availableSystemsIndex.Count];
            for (var i = 0; i < availableSystemsIndex.Count; i++)
                availableSystems[i] = copyParticles[i];

            copyParticles = availableSystems;
            yield return null;
        }

        if (lifeTimeDeletionCoroutine != null)
            StopCoroutine(lifeTimeDeletionCoroutine);
        Stopped?.Invoke(this, null);
        //Destroy(gameObject);
    }


    public event EventHandler Stopped;
}

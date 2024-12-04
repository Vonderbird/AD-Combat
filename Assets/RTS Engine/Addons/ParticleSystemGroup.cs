using System;
using RTSEngine.Effect;
using System.Collections;
using System.Collections.Generic;
using RTSEngine.Event;
using RTSEngine.Game;
using RTSEngine.Utilities;
using UnityEditor;
using UnityEngine;
using RTSEngine.Determinism;
using UnityEngine.Events;
using RTSEngine.Audio;

public class ParticleSystemGroup : MonoBehaviour, IEffectObject
{

    private string code;

    public string Code
    {
        get
        {
            if (string.IsNullOrEmpty(code))
                code = GUID.Generate().ToString();
            return code;
        }
    }
    public EffectObjectState State { private set; get; }
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
    protected IEffectObjectPool effectObjPool { private set; get; }
    protected IGameAudioManager audioMgr { private set; get; }

    #region Raising Events
    public event CustomEventHandler<IEffectObject, EventArgs> EnableEvent;
    private void RaiseEnableEvent()
    {
        var handler = EnableEvent;
        handler?.Invoke(this, EventArgs.Empty);
    }

    public event CustomEventHandler<IEffectObject, EventArgs> DisableEvent;
    private void RaiseDisableEvent()
    {
        var handler = DisableEvent;
        handler?.Invoke(this, EventArgs.Empty);
    }

    public event CustomEventHandler<IEffectObject, EventArgs> DeactivateEvent;
    private void RaiseDeactivateEvent()
    {
        var handler = DeactivateEvent;
        handler?.Invoke(this, EventArgs.Empty);
    }
    #endregion

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

    #region Initializing/Terminating

    public void Init(IGameManager gameMgr)
    {
        EnableEvent?.Invoke(this, null);

        this.globalEvent = gameMgr.GetService<IGlobalEventPublisher>();
        this.effectObjPool = gameMgr.GetService<IEffectObjectPool>();
        this.audioMgr = gameMgr.GetService<IGameAudioManager>();

        //AudioSourceComponent = GetComponent<AudioSource>();

        globalEvent.RaiseEffectObjectCreatedGlobal(this);

        // Create the FollowTransform instance to be used to "parent" the attack object to delay parent or to the target it deals damage to.
        followTransform = new FollowTransform(source: transform, OnFollowTargetInvalid);

        initEvent.Invoke();

        OnEffectObjectInit();
    }

    protected virtual void OnEffectObjectInit() { }

    protected void OnPoolableObjectDestroy()
    {
        globalEvent?.RaiseEffectObjectDestroyedGlobal(this);

        OnEffectObjectDestroy();
    }

    protected virtual void OnEffectObjectDestroy() { }
    #endregion

    private void Start()
    {
        
    }

    public void Play()
    {
        foreach (var system in particleSystems)
            system?.Play();

        if (DeleteOnStop)
            stopDeletionCoroutine = StartCoroutine(DeletedOnStop());

        if (LifeSpan > 0)
            lifeTimeDeletionCoroutine = StartCoroutine(DeleteOnLifeEnds());

    }

    private IEnumerator DeleteOnLifeEnds()
    {
        yield return new WaitForSeconds(LifeSpan);

        if (stopDeletionCoroutine != null)
            StopCoroutine(stopDeletionCoroutine);
        DisableEvent?.Invoke(this, null);
        Destroy(gameObject);
    }

    private IEnumerator DeletedOnStop()
    {
        while (particleSystems.Length > 0)
        {
            List<int> availableSystemsIndex = new();
            for (var i = 0; i < particleSystems.Length; i++)
            {
                if (!particleSystems[i].isPlaying) continue;
                availableSystemsIndex.Add(i);
            }
            var availableSystems = new ParticleSystem[availableSystemsIndex.Count];
            for (var i = 0; i < availableSystemsIndex.Count; i++)
                availableSystems[i] = particleSystems[i];

            particleSystems = availableSystems;
            yield return null;
        }

        if (lifeTimeDeletionCoroutine != null)
            StopCoroutine(lifeTimeDeletionCoroutine);
        Destroy(gameObject);
    }

    public void OnSpawn(EffectObjectSpawnInput input)
    {
        Play();
    }

    protected void OnSpawn(PoolableObjectSpawnInput input)
    {
        transform.SetParent(input.parent, true);

        if (input.useLocalTransform)
            transform.localPosition = input.spawnPosition;
        else
            transform.position = input.spawnPosition;

        input.spawnRotation.Apply(transform, input.useLocalTransform);

        Play();


        if (State != EffectObjectState.inactive)
            return;

        State = EffectObjectState.running;

        //this.enableLifeTime = input.enableLifeTime;
        //if (this.enableLifeTime)
        //{
        //    lastLifeTime = input.useDefaultLifeTime ? defaultLifeTime : input.customLifeTime;
        //    timer = new TimeModifiedTimer(lastLifeTime);
        //}
        //else
        //    lastLifeTime = 0.0f;

        //if (offsetLocalPosition)
        //    transform.localPosition += spawnPositionOffset;
        //else
        //    transform.position += spawnPositionOffset;

        gameObject.SetActive(true);

        enableEvent.Invoke();
        RaiseEnableEvent();

        OnEffectObjectSpawn();
    }
    protected virtual void OnEffectObjectSpawn() { }

    public void Deactivate(bool useDisableTime = true)
    {
        DeletedOnStop();
        DeactivateEvent?.Invoke(this, null);
    }


    protected virtual void OnDeactivated()
    {
        effectObjPool.Despawn(this);
    }

    #region Handling Follow Transform
    // Called when the supposed "parent" object of the attack object that it triggered damage is destroyed.
    private void OnFollowTargetInvalid()
    {
        Deactivate(useDisableTime: false);
    }
    #endregion

    private void OnDestroy()
    {
        OnPoolableObjectDestroy();
    }
}

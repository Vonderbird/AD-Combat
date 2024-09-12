using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemGroup : MonoBehaviour
{
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
}

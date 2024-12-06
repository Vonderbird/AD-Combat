using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RangeAttackParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystemGroup particlePrefab;
    private readonly Queue<ParticleSystemGroup> deactivateParticlesPool = new();
    private ParticleSystemGroup activeParticle;

    public void Enqueue(ParticleSystemGroup item)
    {
        lock (deactivateParticlesPool)
        {
            deactivateParticlesPool.Enqueue(item);
            if (deactivateParticlesPool.Count == 1)
                Monitor.PulseAll(deactivateParticlesPool);
        }
    }

    public ParticleSystemGroup Dequeue()
    {
        lock (deactivateParticlesPool)
        {
            while (deactivateParticlesPool.Count == 0)
                Monitor.Wait(deactivateParticlesPool);

            return deactivateParticlesPool.Dequeue();
        }
    }

    public void Play()
    {
        lock (deactivateParticlesPool)
        {
            if (deactivateParticlesPool.Count == 0)
            {
                activeParticle = Instantiate(particlePrefab, transform);
                activeParticle.Play();
                activeParticle.Stopped += OnParticleStopped;
            }
            else
            {
                activeParticle = deactivateParticlesPool.Peek();
                activeParticle = deactivateParticlesPool.Dequeue();
                activeParticle.transform.localPosition = Vector3.up + Vector3.forward * 2.0f;
                activeParticle.gameObject.SetActive(true);
                activeParticle.Stopped += OnParticleStopped;
                activeParticle.Play();
            }
        }
    }

    private void OnParticleStopped(object sender, EventArgs e)
    {
        if (sender is ParticleSystemGroup s)
        {
            lock (deactivateParticlesPool)
            {
                s.Stopped -= OnParticleStopped;
                s.gameObject.SetActive(false);
                deactivateParticlesPool.Enqueue(s);
            }
        }
    }
}

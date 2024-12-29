using System;
using UnityEngine;

namespace ADC.API
{
    public abstract class ParticleArgs
    {

    }

    public delegate void ParticlePlayerDelegate(ParticlePlayer particlePlayer);
    public abstract class ParticlePlayer : MonoBehaviour
    {
        public ParticlePlayerDelegate Terminated;
        public ParticlePlayer SourcePrefab { get; }
        public abstract void Initialize(ParticlePlayer sourcePrefab, ParticleArgs args);
        public abstract void Play();
        public abstract void Stop();
        public abstract void Reload();
    }
}

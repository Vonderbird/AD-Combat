using UnityEngine;

namespace ADC.API
{
    public abstract class ParticleArgs
    {

    }
    public abstract class ParticlePlayer : MonoBehaviour
    {
        public abstract void Initialize(ParticleArgs args);
        public abstract void Play();
        public abstract void Stop();
    }
}

using System.Collections;
using ADC.API;
using RTSEngine.Determinism;
using Sisus.Init;
using UnityEngine;
using UnityEngine.Events;

namespace ADC.Currencies
{
    [Service(typeof(IWaveTimer), typeof(WaveTimer), FindFromScene = true)]
    public class WaveTimer : MonoBehaviour, IWaveTimer
    {

        [SerializeField] private float period = 30.0f;
        public TimeModifiedTimer Timer { get; private set; } = new();
        public UnityEvent Begin { get; } = new();
        public float Duration => Timer.DefaultValue;
        public float Current => Timer.CurrValue;

        private void Awake()
        {
            Timer = new TimeModifiedTimer(period);
            StartCoroutine(TurnTimeUpdater());
        }
        private IEnumerator TurnTimeUpdater()
        {
            var waitUntil = new WaitUntil(() => Timer.ModifiedDecrease());
            while (true)
            {
                yield return waitUntil;
                Timer.Reload(period);
                Begin?.Invoke();
            }
        }
    }
}
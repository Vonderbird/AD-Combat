using System;
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
        private TimeModifiedTimer Timer { get; set; } = new();
        public event EventHandler<int> Begin;
        public float Duration => Timer.DefaultValue;
        public float Current => Timer.CurrValue;
        private int _tick = 0;
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
                _tick += 1;
                Begin?.Invoke(this, _tick);
            }
        }
    }
}
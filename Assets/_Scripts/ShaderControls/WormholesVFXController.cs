using System;
using ADC.API;
using ADC.Currencies;
using Sisus.Init;
using UnityEngine;
using UnityEngine.VFX;

namespace ADC
{
    public class WormholesVFXController : MonoBehaviour<IWaveTimer>
    {
        [SerializeField] private VisualEffect visualEffect;
        [SerializeField] private bool createOnStart = true;
        private IWaveTimer _waveTimer;

        protected override void Init(IWaveTimer waveTimer)
        {
            _waveTimer = waveTimer;
            OnEnable();
        }

        private void Start()
        {
            if (createOnStart)
                CreatePortal();
        }

        private bool _waveEventAdded = false;
        private void OnEnable()
        {
            if(_waveEventAdded) return;
            _waveTimer.Begin += OnWaveBegin;
            _waveEventAdded = true;
        }

        private void OnDisable()
        {
            if(!_waveEventAdded) return;
            _waveTimer.Begin -= OnWaveBegin;
            _waveEventAdded = false;
        }

        private void OnWaveBegin(object sender, int tick)
        {
            HitPortal();
        }

        public void CreatePortal()
        {
            visualEffect.SendEvent("create");
        }

        public void HitPortal()
        {
            visualEffect.SendEvent("hit");
        }
        public void EndPortal()
        {
            visualEffect.SendEvent("end");
        }

    }
}

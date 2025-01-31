using ADC.API;
using ADC.Currencies;
using UnityEngine;
using UnityEngine.VFX;
using Zenject;

namespace ADC
{
    public class WormholesVFXController : MonoBehaviour
    {
        [SerializeField] private VisualEffect visualEffect;
        [SerializeField] private bool createOnStart = true;
        private IWaveTimer waveTimer;

        [Inject]
        public void Construct(IWaveTimer waveTimer)
        {
            this.waveTimer = waveTimer;
        }

        private void Start()
        {
            if (createOnStart)
                CreatePortal();
        }

        private void OnEnable()
        {
            waveTimer.Begin.AddListener(HitPortal);
        }

        private void OnDisable()
        {
            waveTimer?.Begin.RemoveListener(HitPortal);
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

using ADC.Currencies;
using UnityEngine;
using UnityEngine.VFX;

namespace ADC
{
    public class WormholesVFXController : MonoBehaviour
    {
        [SerializeField] private VisualEffect visualEffect;
        [SerializeField] private bool createOnStart = true;

        private void Start()
        {
            if (createOnStart)
                CreatePortal();
        }

        private void OnEnable()
        {
            EconomySystem.Instance.StartWave.AddListener(HitPortal);
        }

        private void OnDisable()
        {
            if(EconomySystem.HasInstance())
                EconomySystem.Instance.StartWave.RemoveListener(HitPortal);
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

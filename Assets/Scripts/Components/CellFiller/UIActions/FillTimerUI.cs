using System.Collections;
using ADC.API;
using ADC.Currencies;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ADC.UnitCreation
{

    public class FillTimerUI : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private TextMeshProUGUI timer;
        private IWaveTimer waveTimer;

        [Inject]
        public void Construct(IWaveTimer waveTimer)
        {
            this.waveTimer = waveTimer;
        }

        private void Awake()
        {
            StartCoroutine(UpdateFiller());
        }

        IEnumerator UpdateFiller()
        {
            yield return new WaitForSeconds(0.1f);
            while (true)
            {
                
                fillImage.fillAmount = waveTimer.Current / waveTimer.Duration;
                timer.text = $"{waveTimer.Current:f0} Seconds";
                yield return null;
            }
        }
    }
}
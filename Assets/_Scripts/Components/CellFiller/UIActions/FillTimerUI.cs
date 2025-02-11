using System.Collections;
using ADC.API;
using ADC.Currencies;
using Sisus.Init;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ADC.UnitCreation
{

    public class FillTimerUI : MonoBehaviour<IWaveTimer>
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private TextMeshProUGUI timer;
        private IWaveTimer _waveTimer;

        protected override void Init(IWaveTimer waveTimer)
        {
            this._waveTimer = waveTimer;
        }

        protected override void OnAwake()
        {
            StartCoroutine(UpdateFiller());
        }

        IEnumerator UpdateFiller()
        {
            yield return new WaitForSeconds(0.1f);
            while (true)
            {
                
                fillImage.fillAmount = _waveTimer.Current / _waveTimer.Duration;
                timer.text = $"{_waveTimer.Current:f0} Seconds";
                yield return null;
            }
        }

    }
}
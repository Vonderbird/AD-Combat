using System.Collections;
using ADC.Currencies;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ADC.UnitCreation
{

    public class FillTimerUI : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private TextMeshProUGUI timer;

        private void Awake()
        {
            StartCoroutine(UpdateFiller());
        }

        IEnumerator UpdateFiller()
        {
            yield return new WaitForSeconds(0.1f);
            var waveTimer = EconomySystem.Instance.WaveTimer;
            while (true)
            {
                
                fillImage.fillAmount = waveTimer.CurrValue / waveTimer.DefaultValue;
                timer.text = $"{waveTimer.CurrValue:f0} Seconds";
                yield return null;
            }
        }
    }
}
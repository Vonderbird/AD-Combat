using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ADC.UnitCreation
{

    public class FillTimerUI : MonoBehaviour
    {
        [SerializeField] private CellUnitSpawner unitSpawner;
        [SerializeField] private Image fillImage;
        [SerializeField] private TextMeshProUGUI timer;

        private void Awake()
        {
            if (!unitSpawner)
                unitSpawner = FindAnyObjectByType<CellUnitSpawner>();

            StartCoroutine(UpdateFiller());
        }

        IEnumerator UpdateFiller()
        {
            yield return new WaitForSeconds(0.1f);
            while (true)
            {
                fillImage.fillAmount = unitSpawner.UnitsTimer.CurrValue / unitSpawner.UnitsTimer.DefaultValue;
                timer.text = $"{unitSpawner.UnitsTimer.CurrValue:f0} Seconds";
                yield return null;
            }
        }
    }
}
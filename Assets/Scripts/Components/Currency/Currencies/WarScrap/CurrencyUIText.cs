using TMPro;
using UnityEngine;

namespace ADC.Currencies
{

    public abstract class CurrencyUIText<T> : CurrencyInterface<T> where T : struct, ICurrency
    {
        [SerializeField] protected int FloatingPoints = 0;
        [SerializeField] protected TextMeshProUGUI textUI;

        private Coroutine updater;

        private void OnEnable()
        {
            if (textUI == null)
                textUI = GetComponent<TextMeshProUGUI>();
            if (textUI == null)
            {
                Debug.LogError($"[CurrencyUIText] {transform.parent.name} > {name}: textUI is not assigned!");
                //return;
            }
            //updater = StartCoroutine(UpdateUI());
        }

        //private void OnDisable()
        //{
        //    if (updater != null)
        //        StopCoroutine(updater);
        //}

        //IEnumerator UpdateUI()
        //{
        //    while (true)
        //    {
        //        textUI.text = $"{0:F1}";
        //        yield return null;
        //    }
        //}


    }
}
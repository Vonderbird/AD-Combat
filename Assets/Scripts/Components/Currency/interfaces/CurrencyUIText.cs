using TMPro;
using UnityEngine;

namespace ADC.Currencies
{

    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class CurrencyUIText<T> : CurrencyInterface<T> where T : struct, ICurrency
    {
        [SerializeField] protected int FloatingPoints = 0;
        protected TextMeshProUGUI textUI;

        private Coroutine updater;

        protected override void Awake()
        {
            base.Awake();
            textUI = GetComponent<TextMeshProUGUI>();
        }


        private void OnEnable()
        {
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
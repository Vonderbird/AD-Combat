using System;
using ADC.Currencies;
using TMPro;
using UnityEngine;

namespace ADC
{
    public class SimpleUpgradeUI : UpdateUI
    {
        [SerializeField] private TextMeshProUGUI priceText;
        public event EventHandler UnitUpdateClicked;
        public void AddData(WarScrap cost)
        {
            priceText.text = $"{cost}";
        }

        public void OnButtonClicked()
        {
            UnitUpdateClicked?.Invoke(this, null);
        }
    }
}
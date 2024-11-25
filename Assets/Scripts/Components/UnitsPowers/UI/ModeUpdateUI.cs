using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ADC
{
    public class ModeUpdateUI : UpdateUI
    {
        [SerializeField] private Toggle toggleObject;
        [SerializeField] private RectTransform togglesParent;
        Dictionary<string, Toggle> toggles = new();
        private List<Toggle> allToggles = new();
        public event EventHandler<string> ModeChanged;
        public void AddMode(string name, bool isActive)
        {
            var toggleIns = Instantiate(toggleObject, togglesParent);
            toggleIns.gameObject.SetActive(true);
            toggles.Add(name, toggleIns);
            allToggles.Add(toggleIns);
            var text = toggleIns.GetComponentInChildren<TextMeshProUGUI>();
            text.text = name;
            toggleIns.onValueChanged.AddListener((args) => ValueChanged(name, args));

            toggleIns.isOn = isActive;
        }

        private void ValueChanged(string name, bool arg0)
        {
            if (arg0 == false) return;

            allToggles.ForEach(t =>
            {
                if (t != toggles[name])
                {
                    t.isOn = false;
                    t.interactable = true;
                }
            });
            toggles[name].interactable = false;
        }
    }
}
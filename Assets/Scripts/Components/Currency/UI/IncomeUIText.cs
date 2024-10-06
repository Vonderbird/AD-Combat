using System.Linq;
using RTSEngine;
using TMPro;
using UnityEngine;

namespace ADC.Currencies
{
    public class IncomeUIText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI incomeText;
        private FactionEconomy localFaction;
        private bool enabled = false;
        private void Start()
        {
            localFaction = EconomySystem.Instance.FactionsEconomiesDictionary.Where(kv => kv.Key.IsLocalPlayerFaction())
                .Select(kv=>kv.Value).FirstOrDefault();
            OnEnable();
        }

        void OnEnable()
        {
            if(localFaction==null || enabled) return;
            localFaction.IncomeManager.IncomeChanged.AddListener(OnIncomeChanged);
            enabled = true;
        }

        void OnDisable()
        {
            if(!enabled) return;
            localFaction.IncomeManager.IncomeChanged.RemoveListener(OnIncomeChanged);
            enabled = false;
        }

        private void OnIncomeChanged(decimal newIncome)
        {
            incomeText.text = $"{newIncome:F2}";
        }
    }
}
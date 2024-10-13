using System.Linq;
using RTSEngine;
using TMPro;
using UnityEngine;

namespace ADC.Currencies
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class IncomeUIText : MonoBehaviour
    {
        private TextMeshProUGUI incomeText;
        private FactionEconomy targetFaction;
        private bool enabled = false;


        [SerializeField]
        [Tooltip("Left it to default -1 if it is belong to local faction")]
        private int factionId = -1;
        [SerializeField] private int floatingPoints = 0;
        public int FactionId => factionId;

        private void Awake()
        {
            incomeText = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            targetFaction = EconomySystem.Instance.FactionsEconomiesDictionary
                .Where(kv => factionId == -1?kv.Key.IsLocalPlayerFaction(): kv.Key.Equals(factionId))
                .Select(kv=>kv.Value).FirstOrDefault();
            OnEnable();
        }

        void OnEnable()
        {
            if(targetFaction==null || enabled) return;
            targetFaction.IncomeManager.IncomeChanged.AddListener(OnIncomeChanged);
            enabled = true;
        }

        void OnDisable()
        {
            if(!enabled) return;
            targetFaction.IncomeManager.IncomeChanged.RemoveListener(OnIncomeChanged);
            enabled = false;
        }

        private void OnIncomeChanged(decimal newIncome)
        {
            incomeText.text = newIncome.ToString($"n{floatingPoints}");
        }
    }
}
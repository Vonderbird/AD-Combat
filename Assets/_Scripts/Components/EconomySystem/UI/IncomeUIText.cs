using System.Linq;
using ADC.API;
using RTSEngine;
using TMPro;
using UnityEngine;

namespace ADC.Currencies
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class IncomeUIText : MonoBehaviour
    {
        private TextMeshProUGUI incomeText;
        private IFactionEconomy targetFaction;
        private bool isEnabled = false;


        [SerializeField]
        [Tooltip("Left it to default -1 if it is belong to local faction")]
        private int factionId = -1;
        [SerializeField] private int floatingPoints = 0;
        public int FactionId => factionId;

        private IEconomySystem economySystem;

        public void Construct(IEconomySystem economySystem)
        {
            this.economySystem = economySystem;
        }

        private void Awake()
        {
            incomeText = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            targetFaction = economySystem.FactionsEconomiesDictionary
                .Where(kv => factionId == -1?kv.Key.IsLocalPlayerFaction(): kv.Key.Equals(factionId))
                .Select(kv=>kv.Value).FirstOrDefault();
            OnEnable();
        }

        void OnEnable()
        {
            if(targetFaction==null || isEnabled) return;
            targetFaction.IncomeManager.IncomeChanged.AddListener(OnIncomeChanged);
            isEnabled = true;
        }

        void OnDisable()
        {
            if(!isEnabled) return;
            targetFaction.IncomeManager.IncomeChanged.RemoveListener(OnIncomeChanged);
            isEnabled = false;
        }

        private void OnIncomeChanged(decimal newIncome)
        {
            incomeText.text = newIncome.ToString($"n{floatingPoints}");
        }
    }
}
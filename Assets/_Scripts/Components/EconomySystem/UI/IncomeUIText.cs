using System.Linq;
using ADC.API;
using RTSEngine;
using TMPro;
using UnityEngine;
using Zenject;

namespace ADC.Currencies
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class IncomeUIText : MonoBehaviour
    {
        private TextMeshProUGUI incomeText;
        private IFactionEconomy targetFaction;
        private bool enabled = false;


        [SerializeField]
        [Tooltip("Left it to default -1 if it is belong to local faction")]
        private int factionId = -1;
        [SerializeField] private int floatingPoints = 0;
        public int FactionId => factionId;

        private IEconomySystem economySystem;

        [Inject]
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
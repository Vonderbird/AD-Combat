using System.Collections;
using System.Linq;
using ADC.API;
using RTSEngine;
using Sisus.Init;
using TMPro;
using UnityEngine;

namespace ADC.Currencies
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class IncomeUIText : MonoBehaviour<IEconomySystem>
    {
        private TextMeshProUGUI _incomeText;
        private IFactionEconomy _targetFaction;
        private bool _isEnabled = false;


        [SerializeField]
        [Tooltip("Left it to default -1 if it is belong to local faction")]
        private int factionId = -1;
        [SerializeField] private int floatingPoints = 0;
        public int FactionId => factionId;

        private IEconomySystem _economySystem;

        
        protected override void Init(IEconomySystem economySystem)
        {
            this._economySystem = economySystem;
        }

        protected override void OnAwake()
        {
            _incomeText = GetComponent<TextMeshProUGUI>();
        }


        private void Start()
        {
            StartCoroutine(DelayedStart());
        }

        IEnumerator DelayedStart()
        {
            yield return new WaitUntil(() => _economySystem.FactionsEconomiesDictionary != null);
            _targetFaction = _economySystem.FactionsEconomiesDictionary
                .Where(kv => factionId == -1?kv.Key.IsLocalPlayerFaction(): kv.Key.Equals(factionId))
                .Select(kv=>kv.Value).FirstOrDefault();
            OnEnable();
        } 

        void OnEnable()
        {
            if(_targetFaction==null || _isEnabled) return;
            _targetFaction.IncomeManager.IncomeChanged.AddListener(OnIncomeChanged);
            _isEnabled = true;
        }

        void OnDisable()
        {
            if(!_isEnabled) return;
            _targetFaction.IncomeManager.IncomeChanged.RemoveListener(OnIncomeChanged);
            _isEnabled = false;
        }

        private void OnIncomeChanged(decimal newIncome)
        {
            _incomeText.text = newIncome.ToString($"n{floatingPoints}");
        }

    }
}
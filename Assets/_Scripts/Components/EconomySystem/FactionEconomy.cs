using System;
using ADC.API;
using UnityEngine;

namespace ADC.Currencies
{
    [Serializable]
    public class FactionEconomy: IFactionEconomy
    {
        public int FactionId { get; private set; }
        
        private ICurrencyManager<Biofuel> _biofuelManager;
        private ICurrencyManager<WarScrap> _warScrapManager;
        public IIncomeManager IncomeManager { get; private set; }
        [SerializeField] private float initialWarScraps = 200;
        [SerializeField] private float initialBiofuel = 1;
        
        public event EventHandler<CurrencyChangeEventArgs> CurrencyChanged;
        
        public void Init(IIncomeSourceFactory incomeSourceFactory, int factionId)
        {
            FactionId = factionId;
            _biofuelManager = new BiofuelManager(factionId);
            _warScrapManager = new WarScrapManager(factionId);
            _biofuelManager.ValueChanged += (o,e) => CurrencyChanged?.Invoke(o, e.ToNonGeneric());
            _warScrapManager.ValueChanged += (o,e) => CurrencyChanged?.Invoke(o, e.ToNonGeneric());
            IncomeManager = new IncomeManager(incomeSourceFactory, factionId);
            IncomeManager.IncomeReceived += OnIncomeReceived;
        }
        private void OnIncomeReceived(object sender, IncomeEventArgs e)
        {
            Deposit(e.IncomeAmount);
        }

        public void Start()
        {
            _biofuelManager.Init(new Biofuel((decimal)initialBiofuel));
            _warScrapManager.Init(new WarScrap((decimal)initialWarScraps));
        }
        public void OnEnable() { }
        public void OnDisable() { }

        public bool Deposit<T>(T amount) where T : ICurrency
        {
            return amount switch
            {
                Biofuel bf => _biofuelManager.Deposit(bf),
                WarScrap ws => _warScrapManager.Deposit(ws),
                _ => false
            };
        }

        public bool Withdraw<T>(T amount) where T : ICurrency
        {
            return amount switch
            {
                Biofuel bf => _biofuelManager.Withdraw(bf),
                WarScrap ws => _warScrapManager.Withdraw(ws),
                _ => false
            };
        }
        
    }
}
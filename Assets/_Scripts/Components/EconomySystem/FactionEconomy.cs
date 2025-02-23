using System;
using System.Collections.Generic;
using System.Linq;
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
        public HashSet<CurrencyInterface<Biofuel>> BiofuelVisualizers { get; private set; } = new();
        public HashSet<CurrencyInterface<WarScrap>> WarScrapVisualizers { get; private set; } = new();

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

        public void AddVisualizer(CurrencyInterface visualizer)
        {
            switch (visualizer)
            {
                case CurrencyInterface<Biofuel> biofuel:
                    BiofuelVisualizers.Add(biofuel);
                    break;
                case CurrencyInterface<WarScrap> warScrap:
                    WarScrapVisualizers.Add(warScrap);
                    break;
            }
        }

        public void AddVisualizers(List<CurrencyInterface> visualizers)
        {
            BiofuelVisualizers.UnionWith(
                visualizers
                    .Select(v => v as CurrencyInterface<Biofuel>)
                    .Where(v => v));

            WarScrapVisualizers.UnionWith(
                visualizers
                    .Select(v => v as CurrencyInterface<WarScrap>)
                    .Where(v => v));
        }

        public void OnEnable()
        {
            _biofuelManager.ValueChanged += OnBiofuelChanged;
            _warScrapManager.ValueChanged += OnWarScrapChanged;
        }

        public void OnDisable()
        {
            _biofuelManager.ValueChanged += OnBiofuelChanged;
            _warScrapManager.ValueChanged += OnWarScrapChanged;
        }

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
        
        private void OnBiofuelChanged(object o, CurrencyChangeEventArgs<Biofuel> e)
        {
            UpdateVisualizers(o, e);
        }
        private void OnWarScrapChanged(object o, CurrencyChangeEventArgs<WarScrap> e)
        {
            UpdateVisualizers(o, e);
        }
        
        private void UpdateVisualizers(object o, CurrencyChangeEventArgs<Biofuel> e)
        {
            BiofuelVisualizers = BiofuelVisualizers.Where(v => v).ToHashSet();
            foreach (var biofuelVisualizer in BiofuelVisualizers)
            {
                biofuelVisualizer?.Refresh(e);
            }
        }
        private void UpdateVisualizers(object o, CurrencyChangeEventArgs<WarScrap> arg0)
        {
            WarScrapVisualizers = WarScrapVisualizers.Where(v => v).ToHashSet();
            foreach (var warScrapVisualizer in WarScrapVisualizers)
            {
                warScrapVisualizer?.Refresh(arg0);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using ADC.API;
using UnityEngine;
using Zenject;

namespace ADC.Currencies
{

    [Serializable]
    public class FactionEconomy: IFactionEconomy
    {
        public int FactionId { get; private set; }
        
        private BiofuelManager biofuelManager;
        private WarScrapManager warScrapManager;
        public IIncomeManager IncomeManager { get; private set; }
        [SerializeField] private float initialWarScraps = 200;
        [SerializeField] private float initialBiofuel = 0;

        //[SerializeField] private ICurrencyDrainer[] drainers; // !?
        //[SerializeField] private ICurrencyProducer[] sources; // !?

        //[SerializeField] private CurrencyInterface[] visualizers;
        public HashSet<CurrencyInterface<Biofuel>> BiofuelVisualizers { get; private set; } = new();
        public HashSet<CurrencyInterface<WarScrap>> WarScrapVisualizers { get; private set; } = new();
        private IEconomySystem economySystem;


        [Inject]
        public void Init(IIncomeSourceFactory incomeSourceFactory, int factionId)
        {
            FactionId = factionId;
            //visualizers ??= Array.Empty<CurrencyInterface>();
            biofuelManager = new BiofuelManager(factionId);
            warScrapManager = new WarScrapManager(factionId);
            IncomeManager = new IncomeManager(incomeSourceFactory, factionId);
            //AddVisualizers(visualizers);
        }
        
        public void Start()
        {
            biofuelManager.Init(new Biofuel((decimal)initialBiofuel));
            warScrapManager.Init(new WarScrap((decimal)initialWarScraps));

            //var ic = (decimal)initialBiofuel;
            //biofuelManager.Deposit(new Biofuel(ic == 0 ? 0.0000001m : ic));
            //ic = (decimal)initialWarScraps;
            //warScrapManager.Deposit(new WarScrap(ic == 0 ? 0.0000001m : ic));
        }

        public void AddVisualizer(CurrencyInterface visualizer)
        {
            if (visualizer is CurrencyUIText<Biofuel> biofuel)
            {
                BiofuelVisualizers.Add(biofuel);
            }
            else if (visualizer is CurrencyUIText<WarScrap> warScrap)
            {
                WarScrapVisualizers.Add(warScrap);
            }
        }

        public void AddVisualizers(IEnumerable<CurrencyInterface> visualizers)
        {
            BiofuelVisualizers.UnionWith(
                visualizers
                    .Select(v => v as CurrencyUIText<Biofuel>)
                    .Where(v => v));

            WarScrapVisualizers.UnionWith(
                visualizers
                    .Select(v => v as CurrencyUIText<WarScrap>)
                    .Where(v => v));
        }

        public void OnEnable()
        {
            biofuelManager.ValueChanged.AddListener(OnBiofuelChanged);
            biofuelManager.ValueChanged.AddListener(UpdateVisualizers);

            warScrapManager.ValueChanged.AddListener(OnWarScrapChanged);
            warScrapManager.ValueChanged.AddListener(UpdateVisualizers);
        }

        public void OnDisable()
        {
            biofuelManager.ValueChanged.RemoveListener(OnBiofuelChanged);
            biofuelManager.ValueChanged.RemoveListener(UpdateVisualizers);

            warScrapManager.ValueChanged.RemoveListener(OnWarScrapChanged);
            warScrapManager.ValueChanged.RemoveListener(UpdateVisualizers);
        }

        private void UpdateVisualizers(CurrencyChangeEventArgs<Biofuel> arg0)
        {
            BiofuelVisualizers = BiofuelVisualizers.Where(v => v).ToHashSet();
            foreach (var biofuelVisualizer in BiofuelVisualizers)
            {
                biofuelVisualizer?.Refresh(arg0);
            }
        }

        private void UpdateVisualizers(CurrencyChangeEventArgs<WarScrap> arg0)
        {
            WarScrapVisualizers = WarScrapVisualizers.Where(v => v).ToHashSet();
            foreach (var warScrapVisualizer in WarScrapVisualizers)
            {
                warScrapVisualizer?.Refresh(arg0);
            }
        }

        public bool Deposit<T>(T amount) where T : ICurrency
        {
            return amount switch
            {
                Biofuel bf => biofuelManager.Deposit(bf),
                WarScrap ws => warScrapManager.Deposit(ws),
                _ => false
            };
        }

        public bool Withdraw<T>(T amount) where T : ICurrency
        {
            return amount switch
            {
                Biofuel bf => biofuelManager.Withdraw(bf),
                WarScrap ws => warScrapManager.Withdraw(ws),
                _ => false
            };
        }
        
        private void OnBiofuelChanged(CurrencyChangeEventArgs<Biofuel> arg0)
        {
            //Debug.LogError("Biofuel Change not Implemented");
        }

        private void OnWarScrapChanged(CurrencyChangeEventArgs<WarScrap> args0)
        {
            //Debug.LogError("WarScrap Change not Implemented");
        }
    }
}
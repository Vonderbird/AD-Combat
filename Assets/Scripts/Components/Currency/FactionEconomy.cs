using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class FactionEconomy
{
    public int FactionId { get; private set; }
    private BiofuelManager biofuelManager;
    private WarScrapManager warScrapManager;
    [SerializeField] private float initialWarScraps = 200;
    [SerializeField] private float initialBiofuel = 0;

    //[SerializeField] private ICurrencyDrainer[] drainers; // !?
    //[SerializeField] private ICurrencyProducer[] sources; // !?

    //[SerializeField] private CurrencyInterface[] visualizers;
    public HashSet<CurrencyInterface<Biofuel>> BiofuelVisualizers { get; private set; } = new();
    public HashSet<CurrencyInterface<WarScrap>> WarScrapVisualizers { get; private set; } = new();

    public void Init(int factionId)
    {
        FactionId = factionId;
        //visualizers ??= Array.Empty<CurrencyInterface>();
        biofuelManager = new BiofuelManager(factionId);
        warScrapManager = new WarScrapManager(factionId);
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
        Debug.Log("Update Biofuel");
        BiofuelVisualizers = BiofuelVisualizers.Where(v => v).ToHashSet();
        foreach (var biofuelVisualizer in BiofuelVisualizers)
        {
            biofuelVisualizer?.Refresh(arg0);
        }
    }

    private void UpdateVisualizers(CurrencyChangeEventArgs<WarScrap> arg0)
    {
        Debug.Log("Update Biofuel WarScrap");
        WarScrapVisualizers = WarScrapVisualizers.Where(v=>v).ToHashSet();
        foreach (var warScrapVisualizer in WarScrapVisualizers)
        {
            warScrapVisualizer?.Refresh(arg0);
        }
    }

    public bool Deposit(Biofuel amount)
    {
        return biofuelManager.Deposit(amount);
    }

    public bool Deposit(WarScrap amount)
    {
        return warScrapManager.Deposit(amount);
    }

    public bool Withdraw(Biofuel amount)
    {
        return biofuelManager.Withdraw(amount);
    }

    public bool Withdraw(WarScrap amount)
    {
        return warScrapManager.Withdraw(amount);
    }

    private void OnBiofuelChanged(CurrencyChangeEventArgs<Biofuel> arg0)
    {
        Debug.LogError("Biofuel Change not Implemented");
    }

    private void OnWarScrapChanged(CurrencyChangeEventArgs<WarScrap> args0)
    {
        Debug.LogError("WarScrap Change not Implemented");
    }
}
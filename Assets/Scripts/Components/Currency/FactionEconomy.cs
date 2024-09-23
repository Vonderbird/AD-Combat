using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class FactionEconomy
{
    public int FactionId { get; private set; }
    private BiofuelManager biofuelManager;
    [SerializeField] private float initialWarScraps = 200;
    [SerializeField] private float initialBiofuel = 0;

    [SerializeField] private ICurrencyDrainer[] drainers; // !?
    [SerializeField] private ICurrencyProducer[] sources; // !?

    [SerializeField] private CurrencyVisualizer[] visualizers;
    public List<CurrencyVisualizer> Visualizers { get; private set; }
    public void Init(int factionId)
    {
        FactionId = factionId;
        visualizers ??= Array.Empty<CurrencyVisualizer>();
        biofuelManager = new BiofuelManager(factionId, (decimal)initialBiofuel);
        Visualizers = visualizers.ToList();
    }

    public void OnEnable()
    {
        Debug.Log(biofuelManager);
        biofuelManager.ValueChanged.AddListener(OnBiofuelChanged);
        biofuelManager.ValueChanged.AddListener(UpdateVisualizers);
        
        
    }

    public void OnDisable()
    {
        biofuelManager.ValueChanged.RemoveListener(OnBiofuelChanged);
        biofuelManager.ValueChanged.RemoveListener(UpdateVisualizers);
    }

    private void UpdateVisualizers(CurrencyChangeEventArgs<Biofuel> arg0)
    {
        Visualizers = Visualizers.Where(v => v).ToList();
        Visualizers?.ForEach(v => v?.Refresh(arg0));
    }

    public bool Deposit(Biofuel amount)
    {
        return biofuelManager.Deposit(amount);
    }

    public bool Deposit(WarScrap amount)
    {
        return false;
    }

    public bool Withdraw(Biofuel amount)
    {
        return biofuelManager.Withdraw(amount);
    }

    public bool Withdraw(WarScrap amount)
    {
        return false;
    }

    private void OnBiofuelChanged(CurrencyChangeEventArgs<Biofuel> arg0)
    {
        throw new System.NotImplementedException();
    }
}
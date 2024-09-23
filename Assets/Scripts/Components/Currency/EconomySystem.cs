using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using RTSEngine.Game;

public class EconomySystem : MonoBehaviour, IPostRunGameService
{
    [SerializeField] private FactionEconomy[] FactionsEconomies;
    public Dictionary<int, FactionEconomy> FactionsEconomiesDictionary { get; private set; }

    [SerializeField] private CurrencyVisualizer[] GlobalVisualizers;

    private void Awake()
    {
        GlobalVisualizers ??= Array.Empty<CurrencyVisualizer>();

        Debug.Log($"GlobalVisualizers: {GlobalVisualizers}");
        for (int i = 0; i < FactionsEconomies.Length; i++)
        {
            FactionsEconomies[i].Init(i);
            Debug.Log($"Visualizers {i}: {FactionsEconomies[i].Visualizers}");
            FactionsEconomies[i].Visualizers.AddRange(GlobalVisualizers);
        }
        FactionsEconomiesDictionary = FactionsEconomies.ToDictionary(faction => faction.FactionId, faction => faction);
    }


    private void OnEnable()
    {
        foreach (var factionEconomy in FactionsEconomies)
            factionEconomy.OnEnable();
    }
    
    private void OnDisable()
    {
        foreach (var factionEconomy in FactionsEconomies)
            factionEconomy.OnDisable();
    }

    public void Init(IGameManager manager)
    {
    }
}

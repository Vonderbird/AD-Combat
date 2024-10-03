using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using RTSEngine.Game;

public class EconomySystem : Singleton<EconomySystem>, IPostRunGameService
{
    [SerializeField] private FactionEconomy[] FactionsEconomies;
    public Dictionary<int, FactionEconomy> FactionsEconomiesDictionary { get; private set; }

    private List<CurrencyInterface> GlobalVisualizers = new();

    private bool isStarted = false;

    private void Awake()
    {

        for (int i = 0; i < FactionsEconomies.Length; i++)
        {
            FactionsEconomies[i].Init(i);
            FactionsEconomies[i].AddVisualizers(GlobalVisualizers);
        }
        FactionsEconomiesDictionary = FactionsEconomies.ToDictionary(faction => faction.FactionId, faction => faction);
        isStarted = true;
    }

    public FactionEconomy this[int factionId]
    {
        get
        {
            if (FactionsEconomiesDictionary.TryGetValue(factionId, out var f))
            {
                return f;
            }
            Debug.LogError($"[EconomySystem] there is no faction with id {factionId} in economy system!");
            return null;
        }
    }
    private void Start()
    {
        for (int i = 0; i < FactionsEconomies.Length; i++)
        {
            FactionsEconomies[i].Start();
        }
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

    public void Add(CurrencyInterface currencyInterface)
    {
        if (currencyInterface.FactionId == -1)
        {
            GlobalVisualizers.Add(currencyInterface);
        }
        else if(currencyInterface.FactionId < FactionsEconomies.Length)
        {
            FactionsEconomies[currencyInterface.FactionId].AddVisualizer(currencyInterface);
        }
        else
        {
            Debug.LogError($"[EconomySystem] FactionId {currencyInterface.FactionId} is not defined!");
        }

        if (isStarted)
        {
            foreach (var t in FactionsEconomies)
            {
                t.AddVisualizer(currencyInterface);
            }
        }
    }
}

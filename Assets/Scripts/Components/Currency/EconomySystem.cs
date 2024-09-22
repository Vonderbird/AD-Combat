using System;
using RTSEngine.EntityComponent;
using UnityEngine;

public class EconomySystem : Singleton<EconomySystem>
{
    private BiofuelManager biofuelManager;
    [SerializeField] private decimal initialWarScraps = 200;
    [SerializeField] private decimal initialBiofuel = 0;

    [SerializeField] private IDrainer[] drainers;
    [SerializeField] private 

    private void Awake()
    {
        biofuelManager = new BiofuelManager(initialBiofuel);
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

    private void OnEnable()
    {
        biofuelManager.ValueChanged.AddListener(OnBiofuelChanged);
    }

    private void OnDisable()
    {
        biofuelManager.ValueChanged.RemoveListener(OnBiofuelChanged);
    }

    private void OnBiofuelChanged(CurrencyChangeEventArgs<Biofuel> arg0)
    {
        throw new System.NotImplementedException();
    }
}

using UnityEngine;
using UnityEngine.Events;

public interface ICurrencyProducer
{
    void AddToEconomySystem(); // !?
    UnityEvent<Biofuel> BioFuelProduced { get; }
    UnityEvent<WarScrap> WarScrapProduced { get; }
}
using UnityEngine;
using UnityEngine.Events;

public interface ICurrencyDrainer
{
    void AddToEconomySystem(); // !?

    UnityEvent<Biofuel> BioFuelDrained { get; }
    UnityEvent<WarScrap> WarScrapDrained { get; }
}

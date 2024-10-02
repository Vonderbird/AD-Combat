using UnityEngine;
using UnityEngine.Events;

public interface ICurrencyProducer
{
    bool Produce(Biofuel amount);
    bool Produce(WarScrap amount);
}
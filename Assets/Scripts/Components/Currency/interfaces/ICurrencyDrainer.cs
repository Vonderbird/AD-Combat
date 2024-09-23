using UnityEngine.Events;

public interface ICurrencyDrainer
{
    bool Drain(Biofuel amount);
    bool Drain(WarScrap amount);
}

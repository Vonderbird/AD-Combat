using UnityEngine;

namespace ADC
{
    public interface IDefenceType { }

    public class Organic : IDefenceType { }
    public class ScaledPlate : IDefenceType { }
    public class Nano : IDefenceType { }
    public class LightTacticalAssault : IDefenceType { }
    public class CarbonCompound : IDefenceType { }
    public class HeavyPlate : IDefenceType { }
}

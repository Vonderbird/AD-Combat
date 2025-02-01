using System;

namespace ADC.API
{
    public interface IThirdPartyInteractionManager
    {

        event EventHandler<dynamic> TargetUpdated;
        void SetUnitArmor(int value);
        void SetUnitMaxHealth(int value);
        void SetDamage(int unitDamage, int buildingDamage);
        void SetUnitDamage(int value);
        void SetBuildingDamage(int value);
        void SetManaPoint(int value);
    }
}

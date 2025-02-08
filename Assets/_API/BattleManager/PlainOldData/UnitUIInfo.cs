using UnityEngine;

namespace ADC.API
{
    public struct UnitUIInfo
    {
        public IUnitBattleManager Unit;
        public string Title;
        public Sprite UnitBanner;
        public IEquipmentUIInfo Weapon;
        public IEquipmentUIInfo Shield;
        public Sprite[] SpecialAbilityIcons;
        public IUnitUpdateInfo UpdateInfo;
    }
}
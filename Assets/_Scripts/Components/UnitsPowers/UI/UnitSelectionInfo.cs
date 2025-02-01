using System.Linq;
using ADC.API;
using UnityEngine;

namespace ADC
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

    public abstract class UnitSelectionInfo : MonoBehaviour
    {
        public abstract void OnUnitSelected(object sender, SelectionEventArgs args);
        public abstract void OnUnitDeselected(object sender, DeselectionEventArgs args);
        protected UnitUIInfo ExtractUnitUIInfo(IUnitBattleManager unit)
        {
            var UUI = new UnitUIInfo();
            var selectionCatch = unit.GetComponent<UnitSelectionCatch>();
            UUI.Unit = unit;
            UUI.Title = selectionCatch.Title;
            UUI.UnitBanner = selectionCatch.UnitBanner;

            UUI.Weapon = unit.EquipmentManager.Equipments.Weapon.UIInfo;
            UUI.Shield = unit.EquipmentManager.Equipments.Shield.UIInfo;
            UUI.SpecialAbilityIcons = unit.SpecialAbilities.Select(s => s.Icon).ToArray();
            UUI.UpdateInfo = unit.UpdateInfo;
            return UUI;
        }
    }
}
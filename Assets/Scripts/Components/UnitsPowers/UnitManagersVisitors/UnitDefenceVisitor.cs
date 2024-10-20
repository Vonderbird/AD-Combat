using System;
using System.Collections.Generic;

namespace ADC
{
    public class UnitDefenceVisitor: IUnitManagerVisitor
    {
        private readonly Dictionary<Tuple<ArmorType, WeaponType>, float> armorWeaponEffectTable;

        public UnitDefenceVisitor(Dictionary<Tuple<ArmorType, WeaponType>, float> armorWeaponEffectTable)
        {
            this.armorWeaponEffectTable = armorWeaponEffectTable;
        }

        public void Visit(SkyForger skyForger)
        {
            var armor = skyForger.Armor;
            var defence = skyForger.Weapon;
            var targetWeapon = skyForger.Target.Weapon;
            throw new System.NotImplementedException();
        }

        public void Visit(TkArty tkArty)
        {
            throw new System.NotImplementedException();
        }
        public void Visit(Adamnt adamnt)
        {
            throw new System.NotImplementedException();
        }

        private float ArmorDamageReduction(int armorLevel, float armorHealth)
        {
            return (armorLevel * 0.06f) * armorHealth;
        }

        private float WeaponArmorEffectFactor(ArmorType defenderArmor, WeaponType attackerWeapon)
        {
            return this.armorWeaponEffectTable[new Tuple<ArmorType, WeaponType>(defenderArmor, attackerWeapon)];
        }
    }
}
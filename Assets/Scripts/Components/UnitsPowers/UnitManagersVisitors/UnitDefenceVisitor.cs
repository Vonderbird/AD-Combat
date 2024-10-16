using System;
using System.Collections.Generic;

namespace ADC
{
    public class UnitDefenceVisitor: IUnitManagerVisitor
    {
        private readonly Dictionary<Tuple<IArmorType, IWeaponType>, float> armorWeaponEffectTable;

        public UnitDefenceVisitor(Dictionary<Tuple<IArmorType, IWeaponType>, float> armorWeaponEffectTable)
        {
            this.armorWeaponEffectTable = armorWeaponEffectTable;
        }

        public void Visit(SkyForger skyForger)
        {
            var armor = skyForger.ArmorType;
            var defence = skyForger.DefenceType;
            var targetWeapon = skyForger.Target.WeaponType;
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

        private float WeaponArmorEffectFactor(IArmorType defenderArmor, IWeaponType attackerWeapon)
        {
            return this.armorWeaponEffectTable[new Tuple<IArmorType, IWeaponType>(defenderArmor, attackerWeapon)];
        }
    }
}
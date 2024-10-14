using System;
using System.Collections.Generic;

namespace ADC
{
    public class UnitDefence: IUnitVisitor
    {
        private readonly Dictionary<Tuple<IArmor, IWeapon>, float> armorWeaponEffectTable;

        public UnitDefence(Dictionary<Tuple<IArmor, IWeapon>, float> armorWeaponEffectTable)
        {
            this.armorWeaponEffectTable = armorWeaponEffectTable;
        }

        public void Visit(SkyForger skyForger)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(TkArty skyForger)
        {
            throw new System.NotImplementedException();
        }

        private float ArmorDamageReduction(int armorLevel, float armorHealth)
        {
            return (armorLevel * 0.06f) * armorHealth;
        }

        private float WeaponArmorEffectFactor(IArmor defenderArmor, IWeapon attackerWeapon)
        {
            return this.armorWeaponEffectTable[new Tuple<IArmor, IWeapon>(defenderArmor, attackerWeapon)];
        }
    }
}
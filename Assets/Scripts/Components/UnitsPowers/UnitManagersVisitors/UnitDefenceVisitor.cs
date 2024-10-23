using System;
using System.Collections.Generic;

namespace ADC
{
    public class UnitDefenceVisitor: IUnitManagerVisitor
    {
        private readonly Dictionary<Tuple<Shield, Weapon>, float> armorWeaponEffectTable;

        public UnitDefenceVisitor(Dictionary<Tuple<Shield, Weapon>, float> armorWeaponEffectTable)
        {
            this.armorWeaponEffectTable = armorWeaponEffectTable;
        }

        public void Visit(SkyForger skyForger)
        {
            //var armor = skyForger.Shield;
            //var defence = skyForger.Weapon;
            //var targetWeapon = skyForger.Target.Weapon;
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

        public void Visit(Naloxian naloxian)
        {
            throw new NotImplementedException();
        }

        private float ArmorDamageReduction(int armorLevel, float armorHealth)
        {
            return (armorLevel * 0.06f) * armorHealth;
        }

        private float WeaponArmorEffectFactor(Shield defenderShield, Weapon attackerWeapon)
        {
            return this.armorWeaponEffectTable[new Tuple<Shield, Weapon>(defenderShield, attackerWeapon)];
        }
    }
}
using System;
using System.Collections.Generic;
using ADC.API;

namespace ADC
{
    public class UnitDefenceVisitor: IUnitManagerVisitor
    {
        private readonly Dictionary<Tuple<Shield, Weapon>, float> armorWeaponEffectTable;

        public UnitDefenceVisitor(Dictionary<Tuple<Shield, Weapon>, float> armorWeaponEffectTable)
        {
            this.armorWeaponEffectTable = armorWeaponEffectTable;
        }

        public void Visit(AdamantiumLegionSiegeBreaker adamantiumLegionSiegeBreaker)
        {
            //var armor = adamantiumLegionSiegeBreaker.Shield;
            //var defence = adamantiumLegionSiegeBreaker.Weapon;
            //var targetWeapon = adamantiumLegionSiegeBreaker.Target.Weapon;
            throw new System.NotImplementedException();
        }

        public void Visit(ThunderkinArtilleryTank thunderkinArtilleryTank)
        {
            throw new System.NotImplementedException();
        }
        public void Visit(AdamantiumLegionElite adamantiumLegionElite)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(NaloxianBowman naloxianBowman)
        {
            throw new NotImplementedException();
        }

        public void Visit(FrostbornHunter frostbornHunter)
        {
            throw new NotImplementedException();
        }

        public void Visit(ThunderkinDemolitionist thunderkinDemolitionist)
        {
            throw new NotImplementedException();
        }

        public void Visit(DeepwalkerInfilterator deepwalkerInfilterator)
        {
            throw new NotImplementedException();
        }

        public void Visit(FrostbornIceStalker frostbornIceStalker)
        {
            throw new NotImplementedException();
        }

        public void Visit(ThunderkinWarWagon thunderkinWarWagon)
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
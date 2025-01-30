//using System;
//using UnityEngine;

//namespace ADC
//{
//    public class HealingVisitor : IUnitManagerVisitor
//    {
//        private readonly int healingAmount;

//        public HealingVisitor(int healingAmount)
//        {
//            this.healingAmount = healingAmount;
//        }

//        public void Visit(AdamantiumLegionSiegeBreaker adamantiumLegionSiegeBreaker)
//        {
//            adamantiumLegionSiegeBreaker.Specs.Heal(healingAmount * 2);
//            Debug.Log($"Healed AdamantiumLegionSiegeBreaker by {healingAmount * 2} HP.");
//        }

//        public void Visit(ThunderkinArtilleryTank thunderkinArtilleryTank)
//        {
//            thunderkinArtilleryTank.Specs.Heal(healingAmount);
//            Debug.Log($"Healed ThunderkinArtilleryTank by {healingAmount} HP.");
//        }

//        public void Visit(AdamantiumLegionElite adamantiumLegionElite)
//        {
//            adamantiumLegionElite.Specs.Heal(healingAmount / 2);
//            Debug.Log($"Healed AdamantiumLegionElite by {healingAmount / 2} HP.");
//        }

//        public void Visit(NaloxianBowman naloxianBowman)
//        {
//            naloxianBowman.Specs.Heal(healingAmount);
//            Debug.Log($"Healed NaloxianBowman by {healingAmount} HP.");
//        }

//        public void Visit(FrostbornHunter frostbornHunter)
//        {
//            throw new NotImplementedException();
//        }

//        public void Visit(ThunderkinDemolitionist thunderkinDemolitionist)
//        {
//            throw new NotImplementedException();
//        }

//        public void Visit(DeepwalkerInfilterator deepwalkerInfilterator)
//        {
//            throw new NotImplementedException();
//        }

//        public void Visit(FrostbornIceStalker frostbornIceStalker)
//        {
//            throw new NotImplementedException();
//        }

//        public void Visit(ThunderkinWarWagon thunderkinWarWagon)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
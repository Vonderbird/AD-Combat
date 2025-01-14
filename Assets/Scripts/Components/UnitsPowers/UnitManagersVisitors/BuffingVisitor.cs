//using ADC.API;
//using UnityEngine;

//namespace ADC
//{
//    public class BuffingVisitor : MonoBehaviour, IUnitManagerVisitor
//    {
//        [SerializeField] private float buffMultiplier;
//        [SerializeField] private float duration;

//        public void Initialize(float buffMultiplier, float duration)
//        {
//            this.buffMultiplier = buffMultiplier;
//            this.duration = duration;
//        }

//        public void Visit(AdamantiumLegionSiegeBreaker adamantiumLegionSiegeBreaker)
//        {
//            adamantiumLegionSiegeBreaker.Specs.ApplyBuff(
//                new UnitDamage((int)(adamantiumLegionSiegeBreaker.Specs.BaseSpecs.UnitDamage * buffMultiplier)),
//                duration);
//            Debug.Log($"Buffed AdamantiumLegionSiegeBreaker's damage by {buffMultiplier * 100 - 100}% for {duration} seconds.");
//        }

//        public void Visit(ThunderkinArtilleryTank thunderkinArtilleryTank)
//        {
//            thunderkinArtilleryTank.Specs.ApplyBuff(
//                new Armor((int)(thunderkinArtilleryTank.Specs.BaseSpecs.Armor * buffMultiplier)),
//                duration);
//            Debug.Log($"Buffed ThunderkinArtilleryTank's armor by {buffMultiplier * 100 - 100}% for {duration} seconds.");
//        }

//        public void Visit(AdamantiumLegionElite adamantiumLegionElite)
//        {
//            adamantiumLegionElite.Specs.ApplyBuff(
//                new HealthPoint((int)( adamantiumLegionElite.Specs.BaseSpecs.HealthPoint* buffMultiplier)),
//                duration);
//            Debug.Log($"Buffed AdamantiumLegionElite's health by {buffMultiplier * 100 - 100}% for {duration} seconds.");
//        }

//        public void Visit(NaloxianBowman naloxianBowman)
//        {
//            naloxianBowman.Specs.ApplyBuff(
//                new ManaPoint((int)( naloxianBowman.Specs.BaseSpecs.ManaPoint* buffMultiplier)),
//                duration);
//            Debug.Log($"Buffed NaloxianBowman's mana by {buffMultiplier * 100 - 100}% for {duration} seconds.");
//        }

//        public void Visit(FrostbornHunter frostbornHunter)
//        {
//            throw new System.NotImplementedException();
//        }

//        public void Visit(ThunderkinDemolitionist thunderkinDemolitionist)
//        {
//            throw new System.NotImplementedException();
//        }

//        public void Visit(DeepwalkerInfilterator deepwalkerInfilterator)
//        {
//            throw new System.NotImplementedException();
//        }
        
//        public void Visit(FrostbornIceStalker frostbornIceStalker)
//        {
//            throw new System.NotImplementedException();
//        }

//        public void Visit(ThunderkinWarWagon thunderkinWarWagon)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}
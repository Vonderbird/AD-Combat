namespace ADC
{
    public class UnitAttackVisitor : IUnitManagerVisitor
    {
        public void Visit(AdamantiumLegionSiegeBreaker adamantiumLegionSiegeBreaker)
        {
            //adamantiumLegionSiegeBreaker.Weapon.Attack();
            //adamantiumLegionSiegeBreaker.specialAbilities.ForEach(c => c.Play());
        }
        public void Visit(ThunderkinArtilleryTank thunderkinArtilleryTank)
        {
            //tkArty.Weapon.Attack();
        }
        public void Visit(AdamantiumLegionElite adamantiumLegionElite)
        {
            //adamantiumLegionElite.Weapon.Attack();
        }

        public void Visit(Naloxian naloxian)
        {
            //naloxian.Weapon.Attack();
        }

        public void Visit(FrostbornHunter frostbornHunter)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(ThunderkinDemolitionist thunderkinDemolitionist)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(DeepwalkerInfilterator deepwalkerInfilterator)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(FrostbornIceStalker frostbornIceStalker)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(ThunderkinWarWagon thunderkinWarWagon)
        {
            throw new System.NotImplementedException();
        }
    }
}
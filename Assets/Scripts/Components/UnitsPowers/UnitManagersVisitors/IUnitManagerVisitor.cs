namespace ADC
{
    public interface IUnitManagerVisitor
    {
        void Visit(AdamantiumLegionSiegeBreaker adamantiumLegionSiegeBreaker);
        void Visit(AdamantiumLegionElite adamantiumLegionElite);
        void Visit(Naloxian naloxian);
        void Visit(FrostbornHunter frostbornHunter);
        void Visit(ThunderkinDemolitionist thunderkinDemolitionist);
        void Visit(DeepwalkerInfilterator deepwalkerInfilterator);
        void Visit(ThunderkinArtilleryTank thunderkinArtilleryTank);
        void Visit(FrostbornIceStalker frostbornIceStalker);
        void Visit(ThunderkinWarWagon thunderkinWarWagon);
    }
}
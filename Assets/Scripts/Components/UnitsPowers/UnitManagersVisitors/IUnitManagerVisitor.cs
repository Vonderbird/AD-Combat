namespace ADC
{
    public interface IUnitManagerVisitor
    {
        void Visit(SiegeBreaker siegeBreaker);
        void Visit(TkArty tkArty);
        void Visit(Adamnt adamnt);
        void Visit(Naloxian naloxian);
    }
}
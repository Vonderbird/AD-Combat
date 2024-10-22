namespace ADC
{
    public interface IUnitManagerVisitor
    {
        void Visit(SkyForger skyForger);
        void Visit(TkArty tkArty);
        void Visit(Adamnt adamnt);
        void Visit(Naloxian naloxian);
    }
}
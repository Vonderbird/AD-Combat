namespace ADC
{
    public interface IUnitManagerVisitor
    {
        void Visit(SkyForger skyForger);
        void Visit(TkArty skyForger);
    }
}
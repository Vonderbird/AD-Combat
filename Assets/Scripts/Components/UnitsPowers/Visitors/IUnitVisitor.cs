namespace ADC
{
    public interface IUnitVisitor
    {
        void Visit(SkyForger skyForger);
        void Visit(TkArty skyForger);
    }
}
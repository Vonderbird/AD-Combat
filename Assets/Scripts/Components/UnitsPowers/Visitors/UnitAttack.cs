namespace ADC
{
    public class UnitAttack : IUnitVisitor
    {

        public void Visit(SkyForger skyForger){}
        public void Visit(TkArty skyForger)
        {
            throw new System.NotImplementedException();
        }
    }
}
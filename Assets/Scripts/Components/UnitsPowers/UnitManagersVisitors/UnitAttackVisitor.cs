namespace ADC
{
    public class UnitAttackVisitor : IUnitManagerVisitor
    {
        IAttackTypeVisitor attackTypeVisitor;
        public void Visit(SkyForger skyForger){}
        public void Visit(TkArty skyForger)
        {
            attackTypeVisitor.Visit(skyForger.AttackType);
        }
    }
}
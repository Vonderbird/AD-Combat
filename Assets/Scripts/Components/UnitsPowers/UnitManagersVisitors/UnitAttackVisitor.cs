namespace ADC
{
    public class UnitAttackVisitor : IUnitManagerVisitor
    {
        public void Visit(SkyForger skyForger)
        {
            skyForger.AttackType.Attack();
        }
        public void Visit(TkArty tkArty)
        {
            tkArty.AttackType.Attack();
        }
        public void Visit(Adamnt adamnt)
        {
            adamnt.AttackType.Attack();
        }
    }
}
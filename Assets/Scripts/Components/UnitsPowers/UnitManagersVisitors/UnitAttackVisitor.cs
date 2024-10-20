namespace ADC
{
    public class UnitAttackVisitor : IUnitManagerVisitor
    {
        public void Visit(SkyForger skyForger)
        {
            skyForger.Weapon.Attack();
        }
        public void Visit(TkArty tkArty)
        {
            tkArty.Weapon.Attack();
        }
        public void Visit(Adamnt adamnt)
        {
            adamnt.Weapon.Attack();
        }
    }
}
namespace ADC
{
    public class UnitAttackVisitor : IUnitManagerVisitor
    {
        public void Visit(SiegeBreaker siegeBreaker)
        {
            //siegeBreaker.Weapon.Attack();
            //siegeBreaker.specialAbilities.ForEach(c => c.Play());
        }
        public void Visit(TkArty tkArty)
        {
            //tkArty.Weapon.Attack();
        }
        public void Visit(Adamnt adamnt)
        {
            //adamnt.Weapon.Attack();
        }

        public void Visit(Naloxian naloxian)
        {
            //naloxian.Weapon.Attack();
        }
    }
}
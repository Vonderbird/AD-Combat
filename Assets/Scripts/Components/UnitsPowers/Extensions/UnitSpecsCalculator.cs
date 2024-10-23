namespace ADC
{
    public interface IUnitSpecsCalculator
    {
        int CalculateHitPoint();
        int CalculateDamage();
        int CalculateArmor();
        UnitSpecs CalculateAll();
    }


    public class UnitSpecsCalculator: IUnitSpecsCalculator
    {
        public int CalculateHitPoint()
        {
            // attackManager.Weapon + attackManager.Shield + attackManager.XP + attackManager.baseAttack
            //return 0.0f + 0.0f + attackManager.XP.Level * attackManager.;
            return 0;
        }

        public int CalculateDamage()
        {
            throw new System.NotImplementedException();
        }

        public int CalculateArmor()
        {
            throw new System.NotImplementedException();
        }

        public UnitSpecs CalculateAll()
        {
            throw new System.NotImplementedException();
        }
    }
}

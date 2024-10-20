using RTSEngine.EntityComponent;

namespace ADC
{
    public class WeaponTypeEventArgs
    {
        public WeaponType WeaponType { get; set; }
    }
    public class WeaponTypeInitArgs
    {
        public UnitAttack UnitAttack { get; set; }
    }

    public abstract class WeaponType
    {
        protected readonly WeaponTypeInitArgs InitArgs;

        protected WeaponType(WeaponTypeInitArgs initArgs)
        {
            InitArgs = initArgs;
        }
        public abstract void Attack();
    }
}

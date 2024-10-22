namespace ADC
{
    public class BluntAttack : Weapon
    {
        public BluntAttack(WeaponInitArgs initArgs) : base(initArgs)
        {
        }

        public override void Attack()
        {

            throw new System.NotImplementedException();
        }

        public override int Damage { get; set; }

    }
}
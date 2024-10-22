namespace ADC
{
    public class Biological: Weapon
    {
        public Biological(WeaponInitArgs initArgs) : base(initArgs)
        {
        }

        public override void Attack()
        {

            throw new System.NotImplementedException();
        }

        public override int Damage { get; set; }
    }
}
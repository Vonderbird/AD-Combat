namespace ADC
{
    public class Kinetic : Weapon
    {
        public Kinetic(WeaponInitArgs initArgs) : base(initArgs)
        {
        }

        public override void Attack()
        {

            throw new System.NotImplementedException();
        }

        public override int Damage { get; set; }
    }
}
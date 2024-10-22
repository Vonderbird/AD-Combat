namespace ADC
{
    public class Sharpened: Weapon
    {
        public Sharpened(WeaponInitArgs initArgs) : base(initArgs)
        {
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }

        public override int Damage { get; set; }
    }
}
namespace ADC
{
    public class Plasma: Weapon
    {
        public Plasma(WeaponInitArgs initArgs) : base(initArgs)
        {
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }

        public override int Damage { get; set; }

    }
}
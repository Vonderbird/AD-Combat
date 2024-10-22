namespace ADC
{
    public class Incendiary : Weapon
    {
        public Incendiary(WeaponInitArgs initArgs) : base(initArgs)
        {
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }

        public override int Damage { get; set; }

    }
}
namespace ADC
{
    public class Organic : Shield
    {
        public override void Defend()
        {
            throw new System.NotImplementedException();
        }

        public override int Armor { get; }

        public Organic(ShieldInitArgs args) : base(args)
        {
        }
    }
}
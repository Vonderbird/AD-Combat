namespace ADC
{
    public class Nano : Shield
    {
        public override void Defend()
        {
            throw new System.NotImplementedException();
        }

        public override int Armor { get; }

        public Nano(ShieldInitArgs args) : base(args)
        {
        }
    }
}
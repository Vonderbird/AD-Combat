namespace ADC
{
    public class HeavyPlate : Shield
    {
        public override void Defend()
        {
            throw new System.NotImplementedException();
        }

        public override int Armor { get; }

        public HeavyPlate(ShieldInitArgs args) : base(args)
        {
        }
    }
}
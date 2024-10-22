namespace ADC
{
    public class CarbonCompound : Shield
    {
        public override void Defend()
        {
            throw new System.NotImplementedException();
        }

        public override int Armor { get; }

        public CarbonCompound(ShieldInitArgs args) : base(args)
        {
        }
    }
}
namespace ADC
{
    public class ScaledPlate : Shield
    {
        public override void Defend()
        {
            throw new System.NotImplementedException();
        }

        public override int Armor { get; }

        public ScaledPlate(ShieldInitArgs args) : base(args)
        {
        }
    }
}
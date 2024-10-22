namespace ADC
{
    public class LightTacticalAssault : Shield
    {
        public override void Defend()
        {
            throw new System.NotImplementedException();
        }

        public override int Armor { get; }

        public LightTacticalAssault(ShieldInitArgs args) : base(args)
        {
        }
    }
}
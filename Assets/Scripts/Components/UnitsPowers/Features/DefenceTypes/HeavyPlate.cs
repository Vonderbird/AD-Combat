namespace ADC
{
    public class HeavyPlate : ArmorType
    {
        public override void Defend()
        {
            throw new System.NotImplementedException();
        }

        public HeavyPlate(ArmorTypeInitArgs args) : base(args)
        {
        }
    }
}
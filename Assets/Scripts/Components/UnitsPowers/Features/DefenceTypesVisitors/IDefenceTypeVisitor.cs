namespace ADC
{
    public interface IDefenceTypeVisitor
    {
        void Visit(Organic defenceType);
        void Visit(ScaledPlate defenceType);
        void Visit(Nano defenceType);
        void Visit(LightTacticalAssault defenceType);
        void Visit(CarbonCompound defenceType);
        void Visit(HeavyPlate defenceType);
    }
}

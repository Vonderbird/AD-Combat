namespace ADC.Currencies
{

    public interface ICurrencyProducer
    {
        bool Produce(Biofuel amount);
        bool Produce(WarScrap amount);
    }
}
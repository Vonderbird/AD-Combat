namespace ADC.Currencies
{

    public interface ICurrencyDrainer
    {
        bool Drain(Biofuel amount);
        bool Drain(WarScrap amount);
    }
}
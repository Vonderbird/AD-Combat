namespace ADC
{
    public interface ILevel
    {
        int Value { get; }
        float XpToNextLevel(int currentLevel);
    }
}
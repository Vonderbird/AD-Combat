
namespace ADC
{
    public class Level
    {
        public int Value { get; set; }

        public static implicit operator int(Level level)
        {
            return level.Value;
        }
    }

    public class UnitExperience
    {
        public Level Level { get; private set; }
    }
}
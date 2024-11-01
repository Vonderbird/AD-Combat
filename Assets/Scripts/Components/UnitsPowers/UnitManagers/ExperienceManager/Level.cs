using System;

namespace ADC
{
    public class Level: ILevel
    {
        public int Value { get; private set; } = 1;
        private const float ExpFactor = 1.25f;

        public Level(UnitExperience xp)
        {
            xp.XpChanged += OnXpChanged;
        }

        public static implicit operator int(Level level)
        {
            return level.Value;
        }

        public float XpToNextLevel(int currentLevel)
        {
            return 50 * MathF.Pow(ExpFactor, currentLevel - 1);
        }

        public int CalculateLevelUp(int currentLevel, int newXp)
        {
            var currentNextXp = XpToNextLevel(currentLevel);
            var exponentialDifference = newXp / currentNextXp;
            var difference = (int)MathF.Ceiling(MathF.Log(exponentialDifference, ExpFactor));
            return difference;
        }

        private void OnXpChanged(object o, XpChangeEventArgs e)
        {
            if (e.NewXp > XpToNextLevel(Value))
            {
                var levelUp = CalculateLevelUp(Value, e.NewXp);
                Value += levelUp;
                LevelChanged?.Invoke(this, new LevelChangeEventArgs(Value, levelUp));
            }
        }

        public event EventHandler<LevelChangeEventArgs> LevelChanged;
    }
}
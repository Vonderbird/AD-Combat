
using System;

namespace ADC
{
    public class UnitExperience
    {
        public Level Level { get; private set; }
        public int Xp { get; private set; }

        public event EventHandler<XpChangeEventArgs> XpChanged;

        public UnitExperience()
        {
            Level = new Level(this);
        }

        public void AddXp(int addXp)
        {
            Xp += addXp;
            XpChanged?.Invoke(this, new XpChangeEventArgs(Xp, addXp));
        }
    }
}
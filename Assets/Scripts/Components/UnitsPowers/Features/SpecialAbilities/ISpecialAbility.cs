using System;

namespace ADC
{
    public interface ISpecialAbility
    {
        Level Level { get; }
        event EventHandler UnLocked;
        void Use();
        void OnLevelChanged(object sender, LevelChangeEventArgs e);
    }
}

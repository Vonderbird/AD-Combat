using System;

namespace ADC.API
{
    public interface ISpecialAbility
    {
        int UnlockLevel { get; }
        event EventHandler UnLocked;
        void Use();
        //void OnLevelChanged(object sender, LevelChangeEventArgs e);
    }

    public interface IReceivedDamageModifierAbility
    {
        int ModifyReceivedDamage(DamageArgs damage);
    }

    public interface IDealtDamageModifierAbility
    {
        int ModifyDealtDamage(DamageArgs damage);
    }

    public struct DamageArgs
    {
        public DamageArgs(IUnitBattleManager source, IUnitBattleManager target, 
            bool isRanged, bool isArea, int value)
        {
            Source = source;
            IsRanged = isRanged;
            IsArea = isArea;
            Value = value;
            Target = target;
        }

        public IUnitBattleManager Source { get; set; }
        public IUnitBattleManager Target { get; set; }
        public bool IsRanged { get; set; }
        public bool IsArea { get; set; }
        public int Value { get; set; }
    }
}

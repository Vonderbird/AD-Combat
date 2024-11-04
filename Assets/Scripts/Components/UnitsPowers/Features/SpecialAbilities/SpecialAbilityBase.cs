using System;

namespace ADC
{
    public abstract class SpecialAbilityBase : ISpecialAbility
    {
        public Level Level { get; protected set; }
        public event EventHandler UnLocked;

        protected bool isUnlocked;
        protected UnitBattleManager UnitBattleManager;

        protected SpecialAbilityBase(UnitBattleManager unitBattleManager)
        {
            UnitBattleManager = unitBattleManager;
        }

        public virtual void Unlock()
        {
            isUnlocked = true;
            UnLocked?.Invoke(this, EventArgs.Empty);
        }

        public abstract void Use();
        public abstract void OnLevelChanged(object sender, LevelChangeEventArgs e);

    }
}
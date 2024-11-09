using System;
using ADC.API;

namespace ADC
{
    public abstract class SpecialAbilityBase : ISpecialAbility
    {
        public int UnlockLevel { get; protected set; }
        public event EventHandler UnLocked;

        protected bool isUnlocked;
        protected UnitBattleManager UnitBattleManager;

        protected SpecialAbilityBase(UnitBattleManager unitBattleManager, int unlockLevel)
        {
            UnitBattleManager = unitBattleManager;
            UnlockLevel = unlockLevel;
            isUnlocked = unlockLevel == 1;
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
using System;
using ADC.API;
using UnityEngine;

namespace ADC
{
    public abstract class SpecialAbilityBase : ScriptableObject, ISpecialAbility
    {
        [SerializeField] private int unlockLevel = 1;
        public int UnlockLevel => unlockLevel;
        public event EventHandler UnLocked;

        protected bool isUnlocked;
        protected IUnitBattleManager UnitBattleManager;

        public virtual ISpecialAbility Initialize(IUnitBattleManager unitBattleManager)
        {
            UnitBattleManager = unitBattleManager;
            if (unlockLevel <= 1)
                Unlock();
            return this;
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
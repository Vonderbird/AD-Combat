using System;
using ADC.API;
using UnityEngine;

namespace ADC
{
    public abstract class SpecialAbilityBase : ScriptableObject, ISpecialAbility
    {
        [SerializeField] private int unlockLevel = 1;
        [SerializeField] private string name;
        [SerializeField] private Sprite icon;
        public int UnlockLevel => unlockLevel;
        public Sprite Icon => icon;
        public string Name => name;

        public event EventHandler UnLocked;

        protected bool isUnlocked;
        protected IUnitBattleManager UnitBattleManager;


        public virtual ISpecialAbility Initialize(IUnitBattleManager unitBattleManager)
        {
            if (string.IsNullOrEmpty(name))
                name = GetType().Name;
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
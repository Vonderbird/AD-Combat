using System;
using ADC.API;
using Sisus.Init;
using UnityEngine;
using UnityEngine.Serialization;

namespace ADC
{
    public abstract class SpecialAbilityBase : ScriptableObject<IVFXPoolingManager>, ISpecialAbility
    {
        [SerializeField] private int unlockLevel = 1;
        [SerializeField] private string abilityName;
        [SerializeField] private Sprite icon;
        [SerializeField] private GameObject infoCellPrefab;
        public int UnlockLevel => unlockLevel;
        public Sprite Icon => icon;
        public string Name => abilityName;

        public GameObject InfoCellPrefab => infoCellPrefab;

        public event EventHandler UnLocked;

        protected bool isUnlocked;
        protected IUnitBattleManager UnitBattleManager;
        protected IVFXPoolingManager VfxPoolingManager;


        protected override void Init(IVFXPoolingManager vfxPoolingManager)
        {
            Debug.Log($"vfxPoolingManager: {vfxPoolingManager}");
            VfxPoolingManager = vfxPoolingManager;
        }
        
        public virtual ISpecialAbility Initialize(IUnitBattleManager unitBattleManager)
        {
            UnitBattleManager = unitBattleManager;
            if (string.IsNullOrEmpty(abilityName))
                abilityName = GetType().Name;
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
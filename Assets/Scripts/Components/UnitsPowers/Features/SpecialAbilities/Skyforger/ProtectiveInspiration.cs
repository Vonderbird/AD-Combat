using System;
using ADC.API;
using UnityEngine;

namespace ADC
{
    [CreateAssetMenu(fileName = "ProtectiveInspiration", menuName = "ADC/SpecialAbilities/ProtectiveInspiration", order = 99)]
    public class ProtectiveInspiration : SpecialAbilityBase, IReceivedDamageModifierAbility
    {
        [SerializeField] private int addArmor = 3;
        [SerializeField] private ParticlePlayer particlePlayerPrefab;

        public override ISpecialAbility Initialize(IUnitBattleManager unitBattleManager)
        {
            var specialAbility = base.Initialize(unitBattleManager);
            VFXPoolingManager.Instance.SpawnVfx(particlePlayerPrefab,
                new FollowerVfxArgs(transform: unitBattleManager.Transform));
            return specialAbility;
        }

        public override void Use()
        {
            if (!isUnlocked)
            {
                Debug.LogError("Ability is not unlocked.");
                return;
            }

            // Implement specific logic for using Advancing The Pathway
            Debug.Log("Using Advancing The Pathway!");
        }

        public override void OnLevelChanged(object sender, LevelChangeEventArgs e)
        {
            // Implement logic for level changes specific to Advancing The Pathway
            Debug.Log($"Advancing The Pathway leveled up to level {e.NewLevel}!");
        }

        public int ModifyReceivedDamage(DamageArgs damage)
        {
            if (!isUnlocked) return damage.Value;
            return Math.Max(0, damage.Value - addArmor);
        }
    }
}
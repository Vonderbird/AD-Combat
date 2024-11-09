using ADC.API;
using UnityEngine;

namespace ADC
{
    public class BeamBlade : SpecialAbilityBase, IDealtDamageModifierAbility
    {
        public BeamBlade(UnitBattleManager unitBattleManager, int unlockLevel) : base(unitBattleManager, unlockLevel) { }

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

        public int ModifyDealtDamage(DamageArgs dealtDamage)
        {
            if (!isUnlocked) return dealtDamage.Value;
            var rnd = Random.Range(0, 1);
            if (rnd > 0.15f) return dealtDamage.Value;
            var newValue = (int)(dealtDamage.Value * 1.5f);
            // View logic to show changes on UI
            return newValue;
        }

    }
}
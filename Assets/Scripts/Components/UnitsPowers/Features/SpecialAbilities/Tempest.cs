using UnityEngine;

namespace ADC
{
    public class Tempest : SpecialAbilityBase
    {
        public Tempest(UnitBattleManager unitBattleManager, int unlockLevel) : base(unitBattleManager, unlockLevel) { }

        public override void Use()
        {
            if (!isUnlocked)
            {
                Debug.LogError("Ability is not unlocked.");
                return;
            }

            // Implement specific logic for using Tempest
            Debug.Log("Using Tempest!");
        }

        public override void OnLevelChanged(object sender, LevelChangeEventArgs e)
        {
            // Implement logic for level changes specific to Tempest
            Debug.Log($"Tempest leveled up to level {e.NewLevel}!");
        }

    }
}
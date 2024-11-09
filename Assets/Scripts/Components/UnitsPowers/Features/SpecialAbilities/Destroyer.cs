using UnityEngine;

namespace ADC
{
    public class Destroyer : SpecialAbilityBase
    {
        public Destroyer(UnitBattleManager unitBattleManager, int unlockLevel) : base(unitBattleManager, unlockLevel) { }

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
    }
}
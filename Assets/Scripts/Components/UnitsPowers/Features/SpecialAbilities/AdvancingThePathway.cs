using UnityEngine;

namespace ADC
{
    public class AdvancingThePathway : SpecialAbilityBase
    {
        public AdvancingThePathway(UnitBattleManager unitBattleManager) : base(unitBattleManager)
        {
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

    }
}
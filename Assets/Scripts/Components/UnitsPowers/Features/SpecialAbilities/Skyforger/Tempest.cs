using UnityEngine;

namespace ADC
{
    [CreateAssetMenu(fileName = "Tempest", menuName = "ADC/SpecialAbilities/Tempest", order = 99)]
    public class Tempest : SpecialAbilityBase
    {

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
using ADC.API;
using UnityEngine;

namespace ADC
{
    [CreateAssetMenu(fileName = "NaloxianScout", menuName = "ADC/SpecialAbilities/NaloxianScout", order = 99)]
    public class NaloxianScout : SpecialAbilityBase, IDealtDamageModifierAbility
    {
        [SerializeField] private float poisonChance = 0.1f;
        [SerializeField] private int damage = 1;

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
            if (rnd > poisonChance) return dealtDamage.Value;
            var newValue = dealtDamage.Value + damage;
            // View logic to show changes on UI
            return newValue;
        }

    }
}
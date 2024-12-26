using ADC.API;
using UnityEngine;

namespace ADC
{
    [CreateAssetMenu(fileName = "ReuniteArmor", menuName = "ADC/SpecialAbilities/ReuniteArmor", order = 99)]
    public class ReuniteArmor : SpecialAbilityBase, IReceivedDamageModifierAbility
    {
        [SerializeField] private float reduceDamageRatio = 0.3f;

        public override ISpecialAbility Initialize(IUnitBattleManager unitBattleManager)
        {
            reduceDamageRatio = Mathf.Max(0, Mathf.Min(1.0f, reduceDamageRatio));
            return base.Initialize(unitBattleManager);
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
            Debug.Log($"Reunite Armor leveled up to level {e.NewLevel}!");
        }

        public int ModifyReceivedDamage(DamageArgs damage)
        {
            if (!isUnlocked) return damage.Value;
            if ((damage.DamageType & (DamageType.Ranged | DamageType.Melee)) != 0)
            {
                return (int)(damage.Value * (1.0f - reduceDamageRatio));
            }

            return damage.Value;
        }
    }
}
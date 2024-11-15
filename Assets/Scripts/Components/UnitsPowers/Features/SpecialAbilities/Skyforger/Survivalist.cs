using System.Linq;
using ADC.API;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace ADC
{
    [CreateAssetMenu(fileName = "Survivalist", menuName = "ADC/SpecialAbilities/Survivalist", order = 99)]
    public class Survivalist : SpecialAbilityBase, IReceivedDamageModifierAbility
    {

        [SerializeField] private float reduceDamageRatio = 0.3f;

        private readonly HashSet<Type> elementalTypes =
            new(new[] { typeof(Electromagnetic), typeof(Kinetic), typeof(Plasma) });

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
            Debug.Log($"Advancing The Pathway leveled up to level {e.NewLevel}!");
        }

        public int ModifyReceivedDamage(DamageArgs damage)
        {
            if (!isUnlocked) return damage.Value;
            var attackerWeaponType = damage.Source.EquipmentManager.Equipments.Weapon?.GetType();
            if (elementalTypes.Contains(attackerWeaponType))
            {
                return (int)(damage.Value * (1.0f - reduceDamageRatio));
            }

            return damage.Value;
        }
    }
}
using ADC.API;
using RTSEngine.Attack;
using UnityEngine;

namespace ADC
{
    [CreateAssetMenu(fileName = "BeamBlade", menuName = "ADC/SpecialAbilities/BeamBlade", order = 99)]
    public class BeamBlade : SpecialAbilityBase, IDealtDamageModifierAbility
    {
        [SerializeField] private float strikeFactor = 1.5f;
        [SerializeField] private float strikePossibility = 0.15f;

        [SerializeField] private ParticlePlayer hitVFX;

        public override void Use()
        {
            if (!isUnlocked)
            {
                Debug.LogError("Ability is not unlocked.");
                return;
            }

            // Implement specific logic for using Advancing The Pathway
            Debug.Log("Using Advancing The Pathway!");
            hitVFX = UnitBattleManager.Transform.Find("unit_attack").GetComponentInChildren<ParticlePlayer>();
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
            if (rnd > strikePossibility) return dealtDamage.Value;
            var newValue = (int)(dealtDamage.Value * strikeFactor);
            hitVFX?.Hit();
            // View logic to show changes on UI
            return newValue;
        }

    }
}
using ADC.API;
using UnityEngine;

namespace ADC
{
    [CreateAssetMenu(fileName = "AdamantiumArmor", menuName = "ADC/SpecialAbilities/AdamantiumArmor", order = 99)]
    public class AdamantiumArmor : SpecialAbilityBase, IReceivedDamageModifierAbility
    {
        [SerializeField] private float reduceDamageRatio = 0.3f;
        [SerializeField] private ParticlePlayer VFXPrefab;
        [SerializeField] private Vector3 positionOffset = Vector3.up * 0.9f;
        [SerializeField] private Vector3 scaleOffset = Vector3.one;
        private ParticlePlayer vfx;


        public override ISpecialAbility Initialize(IUnitBattleManager unitBattleManager)
        {
            reduceDamageRatio = Mathf.Max(0, Mathf.Min(1.0f, reduceDamageRatio));
            var particleArgs = new FollowerVfxArgs
            {
                Transform = unitBattleManager.Transform,
                AutoCalculateOffset = false,
                PositionOffset = positionOffset,
                ScaleOffset = scaleOffset
            };
            vfx = VFXPoolingManager.Instance.SpawnVfx(VFXPrefab, particleArgs);
            if (isUnlocked)
            {
                vfx?.Play();
                vfx?.Hit();
            }
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
            vfx?.Hit();
            if ((DamageType.Ranged & damage.DamageType) != 0)
            {
                return (int)(damage.Value * (1.0f - reduceDamageRatio));
            }

            return damage.Value;
        }
    }
}
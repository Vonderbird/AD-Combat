using ADC.API;
using UnityEngine;
using UnityEngine.VFX;

namespace ADC
{
    [CreateAssetMenu(fileName = "ReuniteArmor", menuName = "ADC/SpecialAbilities/ReuniteArmor", order = 99)]
    public class ReuniteArmor : SpecialAbilityBase, IReceivedDamageModifierAbility
    {
        [SerializeField] private float reduceDamageRatio = 0.3f;
        [SerializeField] private ParticlePlayer VFXPrefab;
        [SerializeField] private Vector3 positionOffset = Vector3.up*0.9f;
        [SerializeField] private Vector3 scaleOffset = Vector3.one;
        private ParticlePlayer vfx;

        public override ISpecialAbility Initialize(IUnitBattleManager unitBattleManager)
        {
            reduceDamageRatio = Mathf.Max(0, Mathf.Min(1.0f, reduceDamageRatio));
            var particleArgs = new FollowerVfxArgs
            {
                Transform = unitBattleManager.Transform, AutoCalculateOffset = false, PositionOffset = positionOffset,
                ScaleOffset = scaleOffset
            };
            //VFXPoolingManager.Instance.SpawnVfx(VFXPrefab, unitBattleManager.Transform.position, unitBattleManager.Transform.rotation, particleArgs);
            vfx = VFXPoolingManager.Instance.SpawnVfx(VFXPrefab, particleArgs);
            if (isUnlocked)
                vfx?.Play();
            return base.Initialize(unitBattleManager);
        }

        public override void Use()
        {
            if (!isUnlocked)
            {
                Debug.LogError("Ability is not unlocked.");
                return;
            }

            //vfx?.Play();
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
            vfx?.Hit();
            if (!isUnlocked) return damage.Value;
            if ((damage.DamageType & (DamageType.Ranged | DamageType.Melee)) != 0)
            {
                return (int)(damage.Value * (1.0f - reduceDamageRatio));
            }

            return damage.Value;
        }
    }
}
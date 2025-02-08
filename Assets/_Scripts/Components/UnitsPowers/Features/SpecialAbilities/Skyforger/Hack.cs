using System.Collections;
using System.Linq;
using ADC.API;
using ADC.Currencies;
using RTSEngine.Attack;
using RTSEngine.Determinism;
using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using UnityEngine;

namespace ADC
{
    [CreateAssetMenu(fileName = "Hack", menuName = "ADC/SpecialAbilities/Hack", order = 99)]
    public class Hack : SpecialAbilityBase, IHackerDamageModifierAbility
    {
        [SerializeField] private float hackChance = 0.4f;
        [SerializeField] private float hackDuration = 8.0f;
        [SerializeField] private ParticlePlayer TargetHackedVFX;
        [SerializeField] private string baseAttackCode;
        [SerializeField] private string hackAttackCode;
        private TimeModifiedTimer waitForHack;
        private WaitUntil waitUntil;
        private Coroutine hackCoroutine;

        private UnitAttack baseAttack;
        private UnitAttack hackAttack;

        public override ISpecialAbility Initialize(IUnitBattleManager unitBattleManager)
        {
            hackChance = Mathf.Max(0, Mathf.Min(1.0f, hackChance));
            waitForHack = new TimeModifiedTimer(hackDuration);
            waitUntil = new WaitUntil(() => waitForHack.ModifiedDecrease());

            var unitAttacks = unitBattleManager.GetComponentsInChildren<UnitAttack>();
            baseAttack = unitAttacks.FirstOrDefault(ua => ua.Code == baseAttackCode);
            hackAttack = unitAttacks.FirstOrDefault(ua => ua.Code == hackAttackCode);
            //attackDamage = UnitBattleManager.GetComponentsInChildren<UnitAttack>().FirstOrDefault(ua => ua.Code == unitAttackCode);
            return base.Initialize(unitBattleManager);
        }

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
            Debug.Log($"Hack leveled up to level {e.NewLevel}!");
        }

        public int HackThenDamage(DamageArgs damage)
        {
            hackCoroutine = CoroutinePlayer.Instance.StartCoroutine(ProcessTheHack(damage));
            return 0;
        }

        IEnumerator ProcessTheHack(DamageArgs args)
        {
            if (hackAttack == null)
            {
                Debug.LogError($"[GroundPound] damage is not assigned!");
                yield break;
            }

            if (!hackAttack.IsActive)
                yield break;

            var targetUnit = args.Target.GetComponent<FactionEntity>();
            var skinnedMeshRenderer = targetUnit.Model.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var meshRenderer in skinnedMeshRenderer)
            {
                var particleArgs = new SkinnedMeshVfxArgs() { SkinnedMesh = meshRenderer, Lifespan = hackDuration + 0.3f };
                VfxPoolingManager.SpawnVfx(TargetHackedVFX, args.Target.Transform.position, Quaternion.identity, particleArgs);
            }
            yield return waitUntil;

            //damage ??= args.Source.GetComponentInChildren<UnitAttack>().damage;

            var targetPosition = ((args.DamageType & DamageType.Area) != 0) ? args.Source.Transform.position : args.Target.Transform.position;

            var postponeAttack = new PostponeAttack
            {
                IsPostponed = true,
                Source = args.Source.GameObject,
                DamageValue = targetUnit.Health.MaxHealth//args.Target.GetComponent<UnitHealth>().MaxHealth
            };

            hackAttack.damage.Trigger(targetUnit, targetPosition, args.DamageType, postponeAttack);
            //RTSHelper
        }

        public void CancelTheHack()
        {
            if (hackCoroutine == null) return;
            CoroutinePlayer.Instance.StopCoroutine(hackCoroutine);
            hackCoroutine = null;
        }
    }
}
using System.Collections;
using ADC.API;
using ADC.Currencies;
using RTSEngine;
using RTSEngine.Attack;
using RTSEngine.Determinism;
using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace ADC
{
    [CreateAssetMenu(fileName = "Hack", menuName = "ADC/SpecialAbilities/Hack", order = 99)]
    public class Hack : SpecialAbilityBase, IHackerDamageModifierAbility
    {


        [SerializeField] private float hackChance = 0.4f;
        [SerializeField] private float hackDuration = 8.0f;
        private TimeModifiedTimer waitForHack;
        private WaitUntil waitUntil;
        private Coroutine hackCoroutine;
        private AttackDamage damage;

        public override ISpecialAbility Initialize(IUnitBattleManager unitBattleManager)
        {
            hackChance = Mathf.Max(0, Mathf.Min(1.0f, hackChance));
            waitForHack = new TimeModifiedTimer(hackDuration);
            var waitUntil = new WaitUntil(() => waitForHack.ModifiedDecrease());
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
            hackCoroutine = EconomySystem.Instance.StartCoroutine(ProcessTheHack(damage));
            return 0;
        }

        IEnumerator ProcessTheHack(DamageArgs args)
        {
            yield return waitUntil;

            if (damage == null)
                damage = args.Source.GetComponentInChildren<UnitAttack>().Damage;
            var targetPosition = args.IsArea ? args.Source.Transform.position : args.Target.Transform.position;
            damage.Trigger(args.Target.GetComponent<FactionEntity>(), targetPosition, true, true);
            //RTSHelper
        }

        public void CancelTheHack()
        {
            if (hackCoroutine == null) return;
            EconomySystem.Instance.StopCoroutine(hackCoroutine);
            hackCoroutine = null;
        }
    }
}
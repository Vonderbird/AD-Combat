using System.Collections;
using ADC.API;
using RTSEngine;
using RTSEngine.Determinism;
using RTSEngine.EntityComponent;
using UnityEngine;

namespace ADC
{
    [CreateAssetMenu(fileName = "Destroyer", menuName = "ADC/SpecialAbilities/Destroyer", order = 99)]
    public class Destroyer : SpecialAbilityBase, IDealtDamageModifierAbility
    {
        //[SerializeField] private string unitAttackCode = "";
        [SerializeField] private float tickInterval = 0.5f;

        [SerializeField] private DamageType damageType = DamageType.Ranged;

        private UnitAttack attackDamage;

        private TimeModifiedTimer timer;
        private WaitUntil waitUnitNextTick;
        public override ISpecialAbility Initialize(IUnitBattleManager unitBattleManager)
        {
            var specialAbility = base.Initialize(unitBattleManager);
            timer = new TimeModifiedTimer(tickInterval);
            waitUnitNextTick = new WaitUntil(() => timer.ModifiedDecrease());
            attackDamage = UnitBattleManager.GetComponentInChildren<UnitAttack>();
            //attackDamage = UnitBattleManager.GetComponentsInChildren<UnitAttack>().FirstOrDefault(ua => ua.Code == unitAttackCode);
            return specialAbility;
        }

        public override void Use()
        {
            if (!isUnlocked)
            {
                Debug.LogError("Ability is not unlocked.");
                return;
            }

            // Implement specific logic for using Destroyer
            Debug.Log("Using Destroyer!");
        }

        public override void OnLevelChanged(object sender, LevelChangeEventArgs e)
        {
            // Implement logic for level changes specific to Destroyer
            Debug.Log($"Destroyer leveled up to level {e.NewLevel}!");
        }


        public int ModifyDealtDamage(DamageArgs args)
        {
            if (!isUnlocked) return args.Value;

            if (attackDamage == null)
            {
                Debug.LogError($"[Destroyer] attackDamage is not assigned!");
                return 0;
            }
            //attackDamage.Target == args.Target
            // Play
            return args.Value; //newValue;
        }


        IEnumerator AttackTicks()
        {
            yield return waitUnitNextTick;

            if (attackDamage == null)
            {
                Debug.LogError($"[Destroyer] attackDamage is not assigned!");
                yield break;
            }
            if (!attackDamage.Target.IsValid())
            {
                Debug.LogError($"[Destroyer] AttackDamage Target is not assigned!");
                yield break;
            }

        }
    }
}
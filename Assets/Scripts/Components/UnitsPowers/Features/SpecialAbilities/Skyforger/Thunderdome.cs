using System.Collections;
using ADC.API;
using RTSEngine;
using RTSEngine.Determinism;
using RTSEngine.EntityComponent;
using UnityEngine;

namespace ADC
{

    [CreateAssetMenu(fileName = "Thunderdome", menuName = "ADC/SpecialAbilities/Thunderdome", order = 99)]
    public class Thunderdome : SpecialAbilityBase, IDealtDamageModifierAbility
    {
        //[SerializeField] private string unitAttackCode = "";
        [SerializeField] private float shotInterval = 20.0f;
        [SerializeField] private float effectDuration = 2.0f;

        [SerializeField] private DamageType damageType = DamageType.Area | DamageType.Ranged;

        private UnitAttack attackDamage;

        private TimeModifiedTimer timer;
        private WaitUntil waitUnitNextPulse;
        public override ISpecialAbility Initialize(IUnitBattleManager unitBattleManager)
        {
            var specialAbility = base.Initialize(unitBattleManager);
            timer = new TimeModifiedTimer(shotInterval);
            waitUnitNextPulse = new WaitUntil(() => timer.ModifiedDecrease());
            attackDamage = unitBattleManager.GetComponentInChildren<UnitAttack>();
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

            // Implement specific logic for using Thunderdome
            Debug.Log("Using Thunderdome!");
        }

        public override void OnLevelChanged(object sender, LevelChangeEventArgs e)
        {
            // Implement logic for level changes specific to Thunderdome
            Debug.Log($"Thunderdome leveled up to level {e.NewLevel}!");
        }

        public int ModifyDealtDamage(DamageArgs args)
        {
            if (!isUnlocked) return args.Value;

            if (attackDamage == null)
            {
                Debug.LogError($"[Thunderdome] attackDamage is not assigned!");
                return 0;
            }
            //attackDamage.Target == args.Target
            // Play
            return args.Value; //newValue;
        }
        

        IEnumerator AttackPulse()
        {
            yield return waitUnitNextPulse;

            if (attackDamage == null)
            {
                Debug.LogError($"[Thunderdome] attackDamage is not assigned!");
                yield break;
            }
            if (!attackDamage.Target.IsValid())
            {
                Debug.LogError($"[Thunderdome] AttackDamage Target is not assigned!");
                yield break;
            }
        }
    }
}
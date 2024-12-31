using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ADC.API;
using ADC.Currencies;
using RTSEngine.Attack;
using RTSEngine.Determinism;
using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using RTSEngine.Search;
using UnityEngine;

namespace ADC
{
    [CreateAssetMenu(fileName = "GroundPound", menuName = "ADC/SpecialAbilities/GroundPound", order = 99)]
    public class GroundPound : SpecialAbilityBase, IDealtDamageModifierAbility
    {
        //[SerializeField] private string unitAttackCode = "";
        [SerializeField] private float possibility = 0.2f;
        [SerializeField] private float stunDuration = 2.0f;
        [SerializeField] private float RangeOfEffect = 3.0f;

        [SerializeField] private DamageType damageType = DamageType.Area;
        private IGridSearchHandler gridSearch;

        private UnitAttack attackDamage;
        private TimeModifiedTimer stunTimer;
        private WaitUntil waitForStun;

        public override ISpecialAbility Initialize(IUnitBattleManager unitBattleManager)
        {
            var specialAbility = base.Initialize(unitBattleManager);
            attackDamage = UnitBattleManager.GetComponentInChildren<UnitAttack>();
            //attackDamage = UnitBattleManager.GetComponentsInChildren<UnitAttack>().FirstOrDefault(ua => ua.Code == unitAttackCode);
            var gameMgr = EconomySystem.Instance.GameMgr;
            gridSearch = gameMgr.GetService<IGridSearchHandler>();
            stunTimer = new TimeModifiedTimer(stunDuration);
            waitForStun = new WaitUntil(stunTimer.ModifiedDecrease);
            //attackDamage.SetActive(false, false);
            return specialAbility;
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

        public int ModifyDealtDamage(DamageArgs args)
        {
            if (!isUnlocked) return args.Value;
            var rnd = Random.Range(0, 1);
            if (rnd > possibility) return args.Value;

            if (attackDamage == null)
            {
                Debug.LogError($"[GroundPound] attackDamage is not assigned!");
                return 0;
            }
            attackDamage.StartCoroutine(RunAttack(args));
            // Play
            return args.Value; //newValue;
        }

        IEnumerator RunAttack(DamageArgs args)
        {
            //attackDamage.SetActive(true, false);
            var targetPosition = ((damageType & DamageType.Area) != 0) ?
                args.Source.Transform.position : args.Target.Transform.position;
            yield return null;
            gridSearch.Search(
                targetPosition,
                RangeOfEffect,
                -1,
                attackDamage.IsTargetValid,
                // Set to true because we do not want to tie the target to the LOS parameters.
                playerCommand: true,
                out IReadOnlyList<IFactionEntity> targetsInRange);

            foreach (var potentialTarget in targetsInRange)
            {
                potentialTarget.AttackComponents[0].SetActiveLocal(false, false);
            }
            stunTimer.Reload(stunDuration);
            yield return waitForStun;
            foreach (var potentialTarget in targetsInRange)
            {
                potentialTarget.AttackComponents[0].SetActiveLocal(true, false);
            }

            //attackDamage.damage.Trigger(targetUnit, targetPosition, args.DamageType);
        }

        //IEnumerator RunAttack(DamageArgs args)
        //{
        //    var targetUnit = args.Target.GetComponent<FactionEntity>();
        //    var targetPosition = ((damageType & DamageType.Area) != 0) ?
        //        args.Source.Transform.position : args.Target.Transform.position;
        //    yield return null;

        //    damage?.Trigger(targetUnit, targetPosition, args.DamageType);
        //}
    }
}
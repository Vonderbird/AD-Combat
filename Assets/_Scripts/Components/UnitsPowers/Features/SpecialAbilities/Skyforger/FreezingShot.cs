using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ADC.API;
using ADC.Currencies;
using RTSEngine.Attack;
using RTSEngine.Determinism;
using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using RTSEngine.Game;
using RTSEngine.Search;
using UnityEngine;
using Zenject;

namespace ADC
{
    [CreateAssetMenu(fileName = "FreezingShot", menuName = "ADC/SpecialAbilities/FreezingShot", order = 99)]
    public class FreezingShot : SpecialAbilityBase, IDealtDamageModifierAbility
    {
        //[SerializeField] private string unitAttackCode = "";
        [SerializeField] private float possibility = 0.15f;
        [SerializeField] private float duration = 2.0f;
        [SerializeField] private float rangeOfEffect = 5.0f;
        [SerializeField] private DamageType damageType = DamageType.Area;
        [SerializeField] private string baseAttackCode;
        [SerializeField] private string freezingShotAttackCode;

        private Dictionary<string, UnitAttack> attackDamages = new();
        private TimeModifiedTimer timer;
        private WaitUntil waitForTime;
        private IGridSearchHandler gridSearch;

        [Inject]
        public void Construct(IGameManager gameMgr)
        {
            gridSearch = gameMgr.GetService<IGridSearchHandler>();
        }

        public override ISpecialAbility Initialize(IUnitBattleManager unitBattleManager)
        {
            var specialAbility = base.Initialize(unitBattleManager);
            var attacks = unitBattleManager.GetComponentsInChildren<UnitAttack>();
            attackDamages[baseAttackCode] = attacks.FirstOrDefault(a => a.Code == baseAttackCode);
            attackDamages[freezingShotAttackCode] = attacks.FirstOrDefault(a => a.Code == freezingShotAttackCode);
            //attackDamage = unitBattleManager.GetComponentsInChildren<UnitAttack>().FirstOrDefault(ua => ua.Code == unitAttackCode);
            timer = new TimeModifiedTimer(duration);
            waitForTime = new WaitUntil(timer.ModifiedDecrease);
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

            //if (attackDamage == null)
            //{
            //    Debug.LogError($"[GroundPound] attackDamage is not assigned!");
            //    return 0;
            //}

            float rnd = Random.Range(0, 1);
            if (rnd > possibility)
            {
                Debug.Log($"Freezingshot deactivated");
                args.Source.GetComponentsInChildren<UnitAttack>();
                //attackDamages[baseAttackCode].SetActiveLocal(true, true);
                attackDamages[freezingShotAttackCode].SetActiveLocal(false, true);

                return args.Value;
            }

            //attackDamages[baseAttackCode].SetActiveLocal(false, true);
            attackDamages[freezingShotAttackCode].SetActiveLocal(true, true);

            attackDamages[freezingShotAttackCode].StartCoroutine(FreezeTargets(args));
            // Play
            return args.Value; //newValue;
        }

        IEnumerator FreezeTargets(DamageArgs args)
        {
            var targetPosition = ((damageType & DamageType.Area) != 0) ?
                args.Source.Transform.position : args.Target.Transform.position;
            yield return null;
            gridSearch.Search(
                targetPosition,
                rangeOfEffect,
                -1,
                attackDamages[freezingShotAttackCode].IsTargetValid,
                playerCommand: true,
                out IReadOnlyList<IFactionEntity> targetsInRange);

            foreach (var potentialTarget in targetsInRange)
            {
                //potentialTarget.AttackComponents[0].speed?;
                //potentialTarget.MovementComponent.speed?

                var unitMovement = (UnitMovement)(potentialTarget.MovementComponent);
                var speedData = unitMovement.Controller.Data;
                speedData.speed /= 5;
                speedData.angularSpeed /= 5;
                unitMovement.Controller.Data = speedData;

            }
            timer.Reload(duration);
            yield return waitForTime;
            foreach (var potentialTarget in targetsInRange)
            {
                //potentialTarget.AttackComponents[0].speed?;
                var unitMovement = (UnitMovement)(potentialTarget.MovementComponent);
                var speedData = unitMovement.Controller.Data;
                speedData.speed *= 5;
                speedData.angularSpeed *= 5;
                unitMovement.Controller.Data = speedData;
            }
        }
    }
}
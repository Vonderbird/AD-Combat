using System.Collections;
using System.Linq;
using ADC.API;
using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using UnityEngine;

namespace ADC
{
    [CreateAssetMenu(fileName = "GroundPound", menuName = "ADC/SpecialAbilities/GroundPound", order = 99)]
    public class GroundPound : SpecialAbilityBase, IDealtDamageModifierAbility
    {
        [SerializeField] private string unitAttackCode = "";
        [SerializeField] private float possibility = 0.2f;

        [SerializeField] private DamageType damageType = DamageType.Area;

        private UnitAttack attackDamage;

        public override ISpecialAbility Initialize(IUnitBattleManager unitBattleManager)
        {
            var specialAbility = base.Initialize(unitBattleManager);
            //attackDamage = UnitBattleManager.GetComponentInChildren<UnitAttack>();
            attackDamage = UnitBattleManager.GetComponentsInChildren<UnitAttack>().FirstOrDefault(ua => ua.Code == unitAttackCode);
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
            var targetUnit = args.Target.GetComponent<FactionEntity>();
            var targetPosition = ((damageType & DamageType.Area) != 0) ?
                args.Source.Transform.position : args.Target.Transform.position;
            var input = new TargetData<IEntity>() { instance = targetUnit, position = targetPosition };
            yield return null;
            attackDamage.SetTarget(input, false);
            attackDamage.TriggerAttack();
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
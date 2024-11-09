using System;
using System.Linq;
using ADC.API;
using UnityEngine;

namespace ADC
{
    public class UnitSpecsCalculator : BaseUnitSpecsCalculator
    {
        //public override IUnitBattleManager UnitBattleManager { get; }
        private DamageDictionary damageDictionary;
        private void Awake()
        {
            //UnitBattleManager = unitBattleManager;
            damageDictionary = new DamageDictionary();
        }

        public override int CalculateHitPoint()
        {
            // attackManager.Weapon + attackManager.Shield + attackManager.Xp + attackManager.baseAttack
            //return 0.0f + 0.0f + attackManager.Xp.Level * attackManager.;
            return 0;
        }

        public override (int, int) CalculateDamage(IUnitBattleManager attacker, IUnitBattleManager target = null)
        {
            var unitDamageSummation = CalculateUnitDamage(attacker, target);

            var buildingDamageSummation = CalculateBuildingDamage(attacker);

            //if (target)
            //{
            //    buildingDamageSummation *= damageDictionary[
            //        UnitBattleManager.EquipmentManager.Equipments.Shield.GetType(),
            //        target.EquipmentManager.Equipments.Weapon.GetType()
            //    ];
            //}

            return ((int)unitDamageSummation, (int)buildingDamageSummation);
        }

        public override int CalculateBuildingDamage(IUnitBattleManager attacker)
        {
            float buildingDamageSummation = attacker.Specs.BaseSpecs.BuildingDamage +
                                            attacker.EquipmentManager.AttackEquipments
                                                .Sum(equipment => equipment.BuildingDamage);
            return (int)buildingDamageSummation;
        }

        public override int CalculateUnitDamage(IUnitBattleManager attacker, IUnitBattleManager target)
        {
            float unitDamageSummation = attacker.Specs.BaseSpecs.UnitDamage +
                                        attacker.EquipmentManager.AttackEquipments
                                            .Sum(equipment => equipment.UnitDamage);

            if (target != null)
            {
                var armorFactor = MathF.Pow(1 - (MathF.Sign(target.Specs.CurrentSpecs.Armor) * 0.06f),
                    MathF.Abs(target.Specs.CurrentSpecs.Armor));

                unitDamageSummation *= armorFactor * damageDictionary[
                    target.EquipmentManager.Equipments.Shield.GetType(),
                    attacker.EquipmentManager.Equipments.Weapon.GetType()
                ];

            }

            return (int)unitDamageSummation;
        }

        public override float GetRangedDamageFactor(IUnitBattleManager thisBattleComponent, IUnitBattleManager targetBattleComponent)
        {
            Debug.LogError("[UnitSpecsCalculator] not implemented");
            return 1.0f;
        }

        public override int CalculateArmor()
        {
            Debug.LogError("[UnitSpecsCalculator] not implemented");
            return 0;
        }

        public override UnitSpecs CalculateAll()
        {
            Debug.LogError("[UnitSpecsCalculator] not implemented");
            return new UnitSpecs();
        }
    }
}

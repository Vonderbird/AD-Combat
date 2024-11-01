using System;
using System.Linq;
using UnityEngine;

namespace ADC
{
    public interface IUnitSpecsCalculator
    {
        UnitBattleManager UnitBattleManager { get; }
        int CalculateHitPoint();
        (int, int) CalculateDamage(UnitBattleManager target = null);
        int CalculateArmor();
        UnitSpecs CalculateAll();
    }


    public class UnitSpecsCalculator : IUnitSpecsCalculator
    {
        public UnitBattleManager UnitBattleManager { get; }
        private DamageDictionary damageDictionary;
        public UnitSpecsCalculator(UnitBattleManager unitBattleManager)
        {
            UnitBattleManager = unitBattleManager;
            damageDictionary = new DamageDictionary();
        }

        public int CalculateHitPoint()
        {
            // attackManager.Weapon + attackManager.Shield + attackManager.Xp + attackManager.baseAttack
            //return 0.0f + 0.0f + attackManager.Xp.Level * attackManager.;
            return 0;
        }

        public (int, int) CalculateDamage(UnitBattleManager target = null)
        {
            float unitDamageSummation = UnitBattleManager.Specs.BaseSpecs.UnitDamage +
                                  UnitBattleManager.EquipmentManager.AttackEquipments
                                      .Sum(equipment => equipment.UnitDamage);

            if (target != null)
            {
                var armorFactor = MathF.Pow(1 - (MathF.Sign(target.Specs.CurrentSpecs.Armor) * 0.06f),
                    MathF.Abs(target.Specs.CurrentSpecs.Armor));

                unitDamageSummation *= armorFactor * damageDictionary[
                    target.EquipmentManager.Equipments.Shield.GetType(),
                    UnitBattleManager.EquipmentManager.Equipments.Weapon.GetType()
                ];

            }

            float buildingDamageSummation = UnitBattleManager.Specs.BaseSpecs.BuildingDamage +
                                    UnitBattleManager.EquipmentManager.AttackEquipments
                                        .Sum(equipment => equipment.BuildingDamage);

            //if (target)
            //{
            //    buildingDamageSummation *= damageDictionary[
            //        UnitBattleManager.EquipmentManager.Equipments.Shield.GetType(),
            //        target.EquipmentManager.Equipments.Weapon.GetType()
            //    ];
            //}

            return ((int)unitDamageSummation, (int)buildingDamageSummation);
        }

        public int CalculateArmor()
        {
            Debug.LogError("[UnitSpecsCalculator] not implemented");
            return 0;
        }

        public UnitSpecs CalculateAll()
        {
            Debug.LogError("[UnitSpecsCalculator] not implemented");
            return new UnitSpecs();
        }
    }
}

using System;
using System.Linq;

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
            // attackManager.Weapon + attackManager.Shield + attackManager.XP + attackManager.baseAttack
            //return 0.0f + 0.0f + attackManager.XP.Level * attackManager.;
            return 0;
        }

        public (int, int) CalculateDamage(UnitBattleManager target = null)
        {
            float unitDamageSummation = UnitBattleManager.Specs.BaseSpecs.UnitDamage +
                                  UnitBattleManager.EquipmentManager.AttackEquipments
                                      .Sum(equipment => equipment.UnitDamage);

            if (target)
            {
                unitDamageSummation *= damageDictionary[
                    UnitBattleManager.EquipmentManager.Equipments.Shield.GetType(),
                    target.EquipmentManager.Equipments.Weapon.GetType()
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
            throw new System.NotImplementedException();
        }

        public UnitSpecs CalculateAll()
        {
            throw new System.NotImplementedException();
        }
    }
}

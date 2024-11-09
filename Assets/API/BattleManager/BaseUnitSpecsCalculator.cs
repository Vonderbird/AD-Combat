

using UnityEngine;

namespace ADC.API
{
    public abstract class BaseUnitSpecsCalculator: MonoBehaviour
    {
        //public abstract IUnitBattleManager UnitBattleManager { get; }
        public abstract int CalculateHitPoint();
        public abstract int CalculateArmor();
        public abstract UnitSpecs CalculateAll();
        public abstract (int, int) CalculateDamage(IUnitBattleManager attacker, IUnitBattleManager target = null);
        public abstract int CalculateBuildingDamage(IUnitBattleManager attacker);
        public abstract int CalculateUnitDamage(IUnitBattleManager attacker, IUnitBattleManager target);

        public abstract float GetRangedDamageFactor(IUnitBattleManager thisBattleComponent,
            IUnitBattleManager targetBattleComponent);
    }
}
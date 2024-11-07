

namespace ADC.API
{
    public interface IUnitSpecsCalculator
    {
        IUnitBattleManager UnitBattleManager { get; }
        int CalculateHitPoint();
        (int, int) CalculateDamage(IUnitBattleManager target = null);
        int CalculateArmor();
        UnitSpecs CalculateAll();
    }
}
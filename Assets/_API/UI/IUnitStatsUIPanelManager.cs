namespace ADC.API
{
    public interface IUnitStatsUIPanelManager
    {
        void OnUnitSelected(UnitUIInfo unitUIInfo);
        void OnUnitDeselected(IUnitBattleManager unit);
        void OnDealStrikeDamage(int value);
        void OnReceiveStrikeDamage(int value);
    }
}
namespace ADC.API
{
    public delegate void CustomEventHandler<T, E>(T sender, E args);

    public enum SelectionType { single, multiple };

    public struct SelectionEventArgs
    {
        public SelectionType Type { get; set; }
        public IUnitBattleManager SelectedUnit;
        public SelectionEventArgs(SelectionType type, IUnitBattleManager selectedUnit)
        {
            Type = type;
            SelectedUnit = selectedUnit;
        }
    }
    public struct DeselectionEventArgs
    {
        public IUnitBattleManager SelectedUnit;

        public DeselectionEventArgs(IUnitBattleManager selectedUnit)
        {
            SelectedUnit = selectedUnit;
        }
    }

}
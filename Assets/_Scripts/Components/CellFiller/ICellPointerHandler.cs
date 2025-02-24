using UnityEngine.Events;

namespace ADC.UnitCreation
{
    public interface ICellPointerHandler
    {
        bool HoverIsEnable { get; }
        UnityEvent<CellEventArgs> CellSelectiveClicked { get; }
        UnityEvent<CellEventArgs> CellDeletionClicked { get; }
        UnityEvent<CellEventArgs> CellSelectionEntered { get; }
        UnityEvent<CellEventArgs> CellDeletionEntered { get; }
        UnityEvent<CellEventArgs> CellExit { get; }
    }
}
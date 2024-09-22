using JetBrains.Annotations;
using RTSEngine.EntityComponent;

public class ActiveTaskContainer
{
    private UnitCreationTask activeTaskData;
    public bool HasValue { get; set; } = false;
    
    [CanBeNull]
    public UnitCreationTask UnitCreationTask
    {
        get => activeTaskData;
        set
        {
            activeTaskData = value ?? activeTaskData;
            HasValue = value != null;
        }
    }

    public static implicit operator bool(ActiveTaskContainer activeTask)
    {
        return activeTask.HasValue;
    }
}
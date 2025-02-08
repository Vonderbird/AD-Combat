namespace ADC.API
{
    public interface IDeactivablesManager
    {
        void Add(string groupId, IDeactivable deactivable);
        void DeactivateAll();
        void DeactivateGroup(string groupId);
        void DeactivateGroups(string[] groupIds);
    }
}
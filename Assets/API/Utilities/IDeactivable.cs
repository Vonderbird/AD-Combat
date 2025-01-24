namespace ADC.API
{
    public interface IDeactivable
    {
        void AddToManager();
        void Deactivate();
        string[] GroupIds { get; set; }
    }
}
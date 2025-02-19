namespace ADC._Tests.Editor.DIContainer.DummyCodes
{
    public interface IDummyService
    {
        string GetData();
    }

    public class DummyService : IDummyService
    {
        public string GetData() => "Hello";
    }
}
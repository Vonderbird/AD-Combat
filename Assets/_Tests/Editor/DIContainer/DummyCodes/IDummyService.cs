namespace ADC.Editor.Tests
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
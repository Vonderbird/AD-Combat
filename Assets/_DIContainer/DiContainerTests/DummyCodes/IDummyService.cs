

namespace ADC.DIContainerTests
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
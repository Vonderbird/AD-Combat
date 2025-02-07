namespace ADC.Editor.Tests
{
    public class DummyConsumer
    {
        [Inject] public IDummyService Service; // public for easy testing

        // A field that is not marked with [Inject] should not be touched.
        public int NotInjected;
    }
}
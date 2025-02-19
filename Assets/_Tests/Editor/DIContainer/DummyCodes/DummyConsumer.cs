namespace ADC._Tests.Editor.DIContainer.DummyCodes
{
    public class DummyConsumer
    {
        [Inject] public IDummyService Service; // public for easy testing

        // A field that is not marked with [Inject] should not be touched.
        public int NotInjected;
    }
}
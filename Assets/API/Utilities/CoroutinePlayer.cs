namespace ADC.API
{
    public class CoroutinePlayer : Singleton<CoroutinePlayer>
    {
        protected void Awake()
        {
            DestroyOnLoad = false;
        }
    }
}

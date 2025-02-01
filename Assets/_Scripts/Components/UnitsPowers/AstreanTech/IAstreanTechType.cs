namespace ADC
{
    public interface IAstreanTechType
    {
        public abstract void Start();
    }

    public class DefenceDrone: IAstreanTechType
    {
        public void Start()
        {
            throw new System.NotImplementedException();
        }
    }

    public class CryoCannon: IAstreanTechType
    {
        public void Start()
        {
            throw new System.NotImplementedException();
        }
    }
}
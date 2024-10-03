using UnityEngine;

namespace ADC.Currencies
{

    public abstract class CurrencyInterface : MonoBehaviour
    {

        [SerializeField] [Tooltip("Left it to default -1 if it is belong to local faction")]
        private int factionId = -1;

        public int FactionId => factionId;

        private void Awake()
        {
            EconomySystem.Instance.Add(this);
        }
    }

    public abstract class CurrencyInterface<T> : CurrencyInterface where T : struct, ICurrency
    {
        public abstract void Refresh(CurrencyChangeEventArgs<T> args);
    }
}
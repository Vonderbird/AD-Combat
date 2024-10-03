using System;
using ADC.Currencies;
using UnityEngine;

namespace ADC.UnitCreation
{
    [Serializable]
    public class UnitPlacementCosts : MonoBehaviour, ITransactionArgs
    {
        [SerializeField] private float getBioFuel = 0.0f;
        [SerializeField] private float PayWarScrap = 0.0f;
        [SerializeField] private float warScrapPaymentRatio = 0.1f;
        [SerializeField] private float incomePaymentInterval = 30.0f;
        public Biofuel Biofuel => new((decimal)getBioFuel);
        public WarScrap WarScrap => new((decimal)PayWarScrap);
        public float WarScrapPaymentRatio => warScrapPaymentRatio;
        public float IncomePaymentInterval => incomePaymentInterval;
    }
}
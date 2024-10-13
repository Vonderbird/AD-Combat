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
        [SerializeField] private float incomeRatio = 0.1f;
        [SerializeField] private float refundRatio = 0.75f;
        public Biofuel Biofuel => new((decimal)getBioFuel);
        public WarScrap WarScrap => new((decimal)PayWarScrap);
        public decimal IncomeRatio => (decimal)incomeRatio;
        public decimal RefundRatio => (decimal)refundRatio;
    }
}
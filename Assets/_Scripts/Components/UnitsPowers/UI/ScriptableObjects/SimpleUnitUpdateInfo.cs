using System;
using ADC.API;
using ADC.Currencies;
using UnityEngine;
using UnityEngine.Events;

namespace ADC
{
    //[CreateAssetMenu(fileName = "SimpleUnitUpdateInfo", menuName = "ADC/UpdateModes/SimpleUnitUpdateInfo", order = 99)]
    [Serializable]
    public class SimpleUnitUpdateInfo: IUnitUpdateInfo
    {
        [SerializeField] private float cost = 100;
        public WarScrap Cost => new ((decimal)cost);
        public Action OnUpdateAction;

        public UnityEvent OnUpdateClicked2;

        public void OnUpdateClicked()
        {

        }
    }
}
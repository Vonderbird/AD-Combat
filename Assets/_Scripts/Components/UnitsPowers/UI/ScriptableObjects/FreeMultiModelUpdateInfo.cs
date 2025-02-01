using System;
using ADC.API;
using UnityEngine;

namespace ADC
{
    //[CreateAssetMenu(fileName = "FreeMultiModelUpdateInfo", menuName = "ADC/UpdateModes/FreeMultiModelUpdateInfo", order = 99)]
    [Serializable]
    public class FreeMultiModelUpdateInfo: IUnitUpdateInfo
    {
        public string[] Modes;
        public string ActiveMode;
        public Action<string> OnUpdateAction;
    }
}
using ADC.API;
using UnityEngine;

namespace ADC
{
    public class GroundAttackObjects : Singleton<GroundAttackObjects>
    {
        public void AddChild(Transform child)
        {
            child.SetParent(transform);
        }
    }
}

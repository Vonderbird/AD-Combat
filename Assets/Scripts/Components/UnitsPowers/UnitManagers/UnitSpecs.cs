using System;
using UnityEngine;

namespace ADC
{
    [Serializable]
    public struct UnitSpecs
    {
        [SerializeField] public float Armor;
        [SerializeField] public float HealthPoint;
        [SerializeField] public float ManaPoint;
        [SerializeField] public float Damage;
    }
}
using System;
using UnityEngine;

namespace ADC.API
{
    [Serializable]
    public struct HealthPoint : IUnitFeature<int, HealthPoint>
    {
        [SerializeField] private int value;
        public HealthPoint(int value) : this()
        {
            this.value = value;
        }
        public int Value
        {
            get => value;
            set
            {
                if (this.value == value) return;
                this.value = value;
                Changed?.Invoke(this, this);
            }
        }


        public static implicit operator int(HealthPoint hp)
        {
            return hp.value;
        }

        public static HealthPoint operator +(HealthPoint a1, int a2)
        {
            return new HealthPoint(a1.value + a2);
        }
        public static HealthPoint operator -(HealthPoint a1, int a2)
        {
            return new HealthPoint(a1.value - a2);
        }
        public static HealthPoint operator *(HealthPoint a1, int a2)
        {
            return new HealthPoint(a1.value * a2);
        }
        public static HealthPoint operator /(HealthPoint a1, int a2)
        {
            return new HealthPoint(a1.value / a2);
        }

        public event EventHandler<HealthPoint> Changed;
    }
}
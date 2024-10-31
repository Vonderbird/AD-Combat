using System;
using UnityEngine;

namespace ADC
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
            a1.Value += a2;
            return a1;
        }
        public static HealthPoint operator -(HealthPoint a1, int a2)
        {
            a1.Value -= a2;
            return a1;
        }
        public static HealthPoint operator *(HealthPoint a1, int a2)
        {
            a1.Value *= a2;
            return a1;
        }
        public static HealthPoint operator /(HealthPoint a1, int a2)
        {
            a1.Value /= a2;
            return a1;
        }

        public event EventHandler<HealthPoint> Changed;
    }
}
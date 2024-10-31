using System;
using UnityEngine;

namespace ADC
{
    [Serializable]
    public struct ManaPoint : IUnitFeature<int, ManaPoint>
    {
        [SerializeField] private int value;
        public ManaPoint(int value) : this()
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


        public static implicit operator int(ManaPoint mp)
        {
            return mp.value;
        }

        public static ManaPoint operator +(ManaPoint a1, int a2)
        {
            a1.Value += a2;
            return a1;
        }
        public static ManaPoint operator -(ManaPoint a1, int a2)
        {
            a1.Value -= a2;
            return a1;
        }
        public static ManaPoint operator *(ManaPoint a1, int a2)
        {
            a1.Value *= a2;
            return a1;
        }
        public static ManaPoint operator /(ManaPoint a1, int a2)
        {
            a1.Value /= a2;
            return a1;
        }

        public event EventHandler<ManaPoint> Changed;
    }
}
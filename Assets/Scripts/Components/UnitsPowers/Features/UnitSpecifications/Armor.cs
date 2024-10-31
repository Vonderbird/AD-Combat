using System;
using UnityEngine;

namespace ADC
{
    [Serializable]
    public struct Armor : IUnitFeature<int, Armor>
    {
        [SerializeField] private int value;
        public Armor(int value): this()
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

        public static implicit operator int(Armor armor)
        {
            return armor.Value;
        }

        public static Armor operator +(Armor a1, int a2)
        {
            a1.Value += a2;
            return a1;
        }
        public static Armor operator -(Armor a1, int a2)
        {
            a1.Value -= a2;
            return a1;
        }
        public static Armor operator *(Armor a1, int a2)
        {
            a1.Value *= a2;
            return a1;
        }
        public static Armor operator /(Armor a1, int a2)
        {
            a1.Value /= a2;
            return a1;
        }

        public event EventHandler<Armor> Changed;
    }
}
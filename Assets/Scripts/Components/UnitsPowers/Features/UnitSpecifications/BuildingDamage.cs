using System;
using UnityEngine;

namespace ADC
{
    [Serializable]
    public struct BuildingDamage  : IUnitFeature<int, BuildingDamage>
    {
        [SerializeField] private int value;
        public BuildingDamage(int value) : this()
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


        public static implicit operator int(BuildingDamage damage)
        {
            return damage.value;
        }

        public static BuildingDamage operator +(BuildingDamage a1, int a2)
        {
            return new BuildingDamage(a1.value + a2);
        }
        public static BuildingDamage operator -(BuildingDamage a1, int a2)
        {
            return new BuildingDamage(a1.value - a2);
        }
        public static BuildingDamage operator *(BuildingDamage a1, int a2)
        {
            return new BuildingDamage(a1.value * a2);
        }
        public static BuildingDamage operator /(BuildingDamage a1, int a2)
        {
            return new BuildingDamage(a1.value / a2);
        }

        public event EventHandler<BuildingDamage> Changed;
    }
}
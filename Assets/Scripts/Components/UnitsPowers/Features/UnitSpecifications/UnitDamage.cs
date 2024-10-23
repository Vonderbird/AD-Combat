using System;
using UnityEngine;

namespace ADC
{
    [Serializable]
    public struct UnitDamage : IUnitFeature<int, UnitDamage>
    {
        [SerializeField] private int value;
        public UnitDamage(int value) : this()
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


        public static implicit operator int(UnitDamage damage)
        {
            return damage.value;
        }

        public event EventHandler<UnitDamage> Changed;
    }
}
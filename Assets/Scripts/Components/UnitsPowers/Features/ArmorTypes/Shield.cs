using System;
using RTSEngine.Health;
using UnityEngine;

namespace ADC
{
    public class ShieldEventArgs : EventArgs
    {
        public Shield Shield { get; set; }
    }

    public class ShieldInitArgs
    {
        public UnitHealth UnitHealth { get; set; }
    }

    public abstract class Shield: MonoBehaviour, IEquipment<Shield>, IProtectorEquipment
    {
        private readonly ShieldInitArgs args;

        public float Power { get; private set; } // ?
        public float Defence { get; private set; } // ?
        public float Level { get; private set; } // ?
        public float Health { get; private set; } // ?

        protected Shield(ShieldInitArgs args)
        {
            this.args = args;
        }


        public abstract void Defend();
        public abstract int Armor { get; }

    }
}

using System;
using UnityEngine;

namespace ADC.API
{
    public class ShieldEventArgs : EventArgs
    {
        public Shield Shield { get; set; }
    }

    public class ShieldInitArgs
    {
        //public UnitHealth UnitHealth { get; set; }
    }

    public abstract class Shield: MonoBehaviour, IEquipment<Shield>, IProtectorEquipment
    {

        [SerializeField] private int armor;

        public virtual int Armor
        {
            get => armor;
            protected set => armor = value;
        }

        protected ShieldInitArgs Args;

        public float Power { get; private set; } // ?
        public float Defence { get; private set; } // ?
        public float Level { get; private set; } // ?
        public float Health { get; private set; } // ?
        public static Shield Default { get; private set; } 

        public void Initialize(ShieldInitArgs args)
        {
            Args = args;
            Default = new GameObject("NoShield").AddComponent<NoShield>();
        }


        public abstract void Defend();
    }

    public class NoShield: Shield
    {
        public override void Defend()
        {
            throw new NotImplementedException();
        }
    }
}

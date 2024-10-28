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

        public void Initialize(ShieldInitArgs args)
        {
            Args = args;
        }


        public abstract void Defend();
        public abstract void Defend(Biological weapon);
        public abstract void Defend(BluntAttack weapon);
        public abstract void Defend(ExplosiveRounds weapon);
        public abstract void Defend(Incendiary weapon);
        public abstract void Defend(Kinetic weapon);
        public abstract void Defend(Plasma weapon);
        public abstract void Defend(Sharpened weapon);
    }
}

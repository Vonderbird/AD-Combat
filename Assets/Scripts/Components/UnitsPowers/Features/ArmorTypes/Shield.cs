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
        public static Shield Default { get; } = new NoShield();

        public void Initialize(ShieldInitArgs args)
        {
            Args = args;
        }


        public abstract void Defend();
        public abstract void Defend(Biological weapon);
        public abstract void Defend(ElectroMagnetic weapon);
        public abstract void Defend(ExplosiveRounds weapon);
        public abstract void Defend(Sharpened weapon);
        public abstract void Defend(Kinetic weapon);
        public abstract void Defend(Plasma weapon);
    }

    public class NoShield: Shield
    {
        public override void Defend()
        {
            throw new NotImplementedException();
        }

        public override void Defend(Biological weapon)
        {
            throw new NotImplementedException();
        }

        public override void Defend(ElectroMagnetic weapon)
        {
            throw new NotImplementedException();
        }

        public override void Defend(ExplosiveRounds weapon)
        {
            throw new NotImplementedException();
        }

        public override void Defend(Sharpened weapon)
        {
            throw new NotImplementedException();
        }

        public override void Defend(Kinetic weapon)
        {
            throw new NotImplementedException();
        }

        public override void Defend(Plasma weapon)
        {
            throw new NotImplementedException();
        }
    }
}

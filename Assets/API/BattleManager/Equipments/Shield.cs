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

    [Serializable]
    public struct ShieldUIInfo
    {
        public string Title;
        public Sprite Icon;
    }

    public abstract class Shield: MonoBehaviour, IEquipment<Shield>, IProtectorEquipment
    {

        [SerializeField] private int armor;
        [SerializeField] private ShieldUIInfo uiInfo;
        public ShieldUIInfo UIInfo => uiInfo;

        public virtual int Armor
        {
            get => armor;
            protected set => armor = value;
        }

        //protected ShieldInitArgs Args;

        public float Power { get; private set; } // ?
        public float Defence { get; private set; } // ?
        public float Level { get; private set; } // ?
        public float Health { get; private set; } // ?
        private static Shield defaultShield;
        private static object lockObj = new();

        public static Shield Default
        {
            get
            {
                lock(lockObj)
                {
                    if (defaultShield)
                        defaultShield = new GameObject("NoShield").AddComponent<NoShield>();
                }
                return defaultShield;
            }
        }

        public void Awake() //Initialize(ShieldInitArgs args)
        {
            //Args = args;
            if (string.IsNullOrEmpty(uiInfo.Title))
                uiInfo.Title = GetType().Name;
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

using System;
using UnityEngine;

namespace ADC
{

    [Serializable]
    public class UnitEquipments
    {
        [SerializeField] private Shield shield;
        [SerializeField] private Weapon weapon;

        public Shield Shield 
        {
            get => shield ?? Shield.Default;
            private set
            {
                Debug.Log($"shield: {shield} : {value}");
                shield = value;
            }
        }

        public Weapon Weapon
        {
            get => weapon ?? Weapon.Default;
            private set
            {
                Debug.Log($"weapon: {weapon} : {value}");
                weapon = value;
            }
        }

        public void Update(Shield shield)
        {
            Shield = shield;
        }

        public void Update(Weapon weapon)
        {
            Weapon = weapon;
        }

        public override bool Equals(object obj)
        {
            if (obj is UnitEquipments other)
            {
                return Equals(shield, other.shield) && Equals(weapon, other.weapon);
            }
            return false;
        }


        public override int GetHashCode()
        {
            return (shield?.GetHashCode() ?? 0) ^ (weapon?.GetHashCode() ?? 0);
        }
    }
}

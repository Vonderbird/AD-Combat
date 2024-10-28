using System;
using UnityEngine;

namespace ADC
{

    [Serializable]
    public struct UnitEquipments
    {
        [SerializeField] private Shield shield;
        [SerializeField] private Weapon weapon;

        public Shield Shield => shield;
        public Weapon Weapon => weapon;

        public void Update(UnitEquipments unitEquipments)
        {
            shield = unitEquipments.Shield;
            weapon = unitEquipments.Weapon;
        }

        public void Update(Shield shield)
        {
            this.shield = shield;
        }

        public void Update(Weapon weapon)
        {
            this.weapon = weapon;
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

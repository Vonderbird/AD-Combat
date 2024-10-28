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
    }
}

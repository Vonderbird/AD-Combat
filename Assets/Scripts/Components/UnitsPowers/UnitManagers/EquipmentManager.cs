using System;
using System.Collections.Generic;

namespace ADC
{
    public class EquipmentManager
    {
        public Shield Shield { get; private set; }
        public Weapon Weapon { get; private set; }

        private HashSet<IProtectorEquipment> protectiveEquipments;
        private HashSet<IAttackEquipment> attackEquipments;

        // Call after transaction! or Inventory Equipment! or ...

        public void SetWeapon(Weapon weapon)
        {
            if(Weapon!=null)
                RemoveEquipment(Weapon);
            Weapon = weapon;
            AddEquipment(Weapon);

            weaponEventArgs.Weapon = weapon;
            WeaponChanged?.Invoke(this, weaponEventArgs);

            // set RTS-Engine weapon objects and settings
            // Modify RTS-Engine damage, health etc.

            //SetUnitDamage(weapon);
            //if (Weapon is IProtectorEquipment a)
            //{
            //    SetUnitArmor(a);
            //}
            //unitHealth.SetMax(new HealthUpdateArgs(1000, unit));

        }

        private void AddEquipment(IEquipment equipment)
        {
            var isAdded = false;
            if (equipment is IProtectorEquipment p)
            {
                protectiveEquipments.Add(p);
                isAdded = true;
            }

            if (equipment is IAttackEquipment a)
            {
                attackEquipments.Add(a);
                isAdded = true;
            }

            if (isAdded)
                EquipmentAdded?.Invoke(this, new EquipmentEventArgs(equipment));
        }

        private void RemoveEquipment(IEquipment equipment)
        {
            var isRemoved = false;
            if (equipment is IProtectorEquipment p)
            {
                protectiveEquipments.Remove(p);
                isRemoved = true;
            }

            if (equipment is IAttackEquipment a)
            {
                attackEquipments.Remove(a);
                isRemoved = true;
            }
            if(isRemoved)
                EquipmentRemoved?.Invoke(this, new EquipmentEventArgs(equipment));
        }

        // Call after transaction! or Inventory Equipment! or ...
        public void SetShield(Shield shield)
        {
            Shield = shield;
            shieldEventArgs.Shield = shield;
            ArmorChanged?.Invoke(this, shieldEventArgs);

            //SetUnitArmor();
            // set RTS-Engine shield objects and settings
            // Modify RTS-Engine damage, health etc.
        }


        public event EventHandler<EquipmentEventArgs> EquipmentRemoved;
        public event EventHandler<EquipmentEventArgs> EquipmentAdded;

        private readonly WeaponEventArgs weaponEventArgs = new();
        public event EventHandler<WeaponEventArgs> WeaponChanged;

        private readonly ShieldEventArgs shieldEventArgs = new();
        public event EventHandler<ShieldEventArgs> ArmorChanged;

    }
}
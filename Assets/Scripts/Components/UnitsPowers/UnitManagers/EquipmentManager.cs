using System;
using System.Collections.Generic;
using RTSEngine.Attack;
using RTSEngine.Entities;
using RTSEngine.EntityComponent;
using RTSEngine.Event;
using RTSEngine.Health;

namespace ADC
{
    public class EquipmentManager
    {
        private readonly UnitAttack unitAttack;
        private readonly UnitHealth unitHealth;
        private readonly IUnit unit;

        public EquipmentManager(UnitAttack unitAttack, UnitHealth unitHealth)
        {
            this.unitAttack = unitAttack;
            this.unitHealth = unitHealth;
            unit = unitHealth.Unit;
        }
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
            if (equipment is IProtectorEquipment p)
            {
                protectiveEquipments.Add(p);
            }

            if (equipment is IAttackEquipment a)
            {
                attackEquipments.Add(a);
            }
        }

        private void RemoveEquipment(IEquipment equipment)
        {
            if (equipment is IProtectorEquipment p)
            {
                protectiveEquipments.Remove(p);
            }

            if (equipment is IAttackEquipment a)
            {
                attackEquipments.Remove(a);
            }
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

        public void SetUnitArmor(IProtectorEquipment value)
        {

        }

        public void SetUnitMaxHealth(IHitPointEquipment value)
        {

        }

        public void SetUnitDamage(IAttackEquipment value)
        {
            unitAttack.Damage.UpdateDamage(new DamageData() { building = 1000, unit = 1000 });
        }


        private readonly WeaponEventArgs weaponEventArgs = new();
        public event EventHandler<WeaponEventArgs> WeaponChanged;

        private readonly ShieldEventArgs shieldEventArgs = new();
        public event EventHandler<ShieldEventArgs> ArmorChanged;

    }
}
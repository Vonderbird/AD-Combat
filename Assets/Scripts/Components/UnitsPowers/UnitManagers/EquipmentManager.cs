using System;
using System.Collections.Generic;
using ADC.API;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ADC
{
    

    public class EquipmentManager: IEquipmentManager
    {
        public UnitEquipments Equipments { get; private set; } = new();

        public HashSet<IProtectorEquipment> ProtectiveEquipments { get; } = new();
        public HashSet<IAttackEquipment> AttackEquipments { get; } = new();

        public UnitSpecs AddedSpecs { get; private set; } = new();

        // Call after transaction! or Inventory Equipment! or ...

        public void SetWeapon(Weapon weapon)
        {
            if(Equipments.Weapon != null)
                RemoveEquipment(weapon);
            var w = Object.Instantiate(weapon, unitBattleManger.transform, false);
            Equipments.Update(w);
            AddEquipment(w);

            weaponEventArgs.Weapon = w;
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

        public void SetShield(Shield shield)
        {
            if (Equipments.Shield != null)
                RemoveEquipment(shield);
            var sh = Object.Instantiate(shield, unitBattleManger.transform, false);
            Equipments.Update(sh);
            AddEquipment(sh);

            shieldEventArgs.Shield = sh;
            ArmorChanged?.Invoke(this, shieldEventArgs);

            //SetUnitArmor();
            // set RTS-Engine shield objects and settings
            // Modify RTS-Engine damage, health etc.
        }
        
        private void AddEquipment(IEquipment equipment)
        {
            var isAdded = false;
            if (equipment is IProtectorEquipment p)
            {
                ProtectiveEquipments.Add(p);
                AddedSpecs.Armor += p.Armor;
                isAdded = true;
            }

            if (equipment is IAttackEquipment a)
            {
                AttackEquipments.Add(a);
                AddedSpecs.UnitDamage += a.UnitDamage;
                AddedSpecs.BuildingDamage += a.BuildingDamage;
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
                ProtectiveEquipments.Remove(p);
                AddedSpecs.Armor -= p.Armor;
                isRemoved = true;
            }

            if (equipment is IAttackEquipment a)
            {
                AttackEquipments.Remove(a);
                AddedSpecs.UnitDamage -= a.UnitDamage;
                AddedSpecs.BuildingDamage -= a.BuildingDamage;
                isRemoved = true;
            }
            if(isRemoved)
                EquipmentRemoved?.Invoke(this, new EquipmentEventArgs(equipment));
        }

        // Call after transaction! or Inventory Equipment! or ...
        
        public void UpdateEquipments(UnitSpecs baseSpecs, UnitEquipments targetEquipments) // , UnitEquipments baseEquipments );
        {
            SetShield(targetEquipments.Shield);
            SetWeapon(targetEquipments.Weapon);
            // Calculate Added Specs
        }

        public event EventHandler<EquipmentEventArgs> EquipmentRemoved;
        public event EventHandler<EquipmentEventArgs> EquipmentAdded;

        private readonly WeaponEventArgs weaponEventArgs = new();
        public event EventHandler<WeaponEventArgs> WeaponChanged;

        private readonly ShieldEventArgs shieldEventArgs = new();
        private readonly UnitBattleManager unitBattleManger;

        public EquipmentManager(UnitBattleManager unitBattleManager)
        {
            this.unitBattleManger = unitBattleManager;
        }

        public event EventHandler<ShieldEventArgs> ArmorChanged;

    }

}
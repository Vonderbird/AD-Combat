using System;
using UnityEngine;

namespace ADC.API
{
    public struct EquipmentEventArgs
    {
        public EquipmentEventArgs(IEquipment equipment)
        {
            Equipment = equipment;
        }

        public IEquipment Equipment { get; set; }
    }
    public class WeaponEventArgs
    {
        public Weapon Weapon { get; set; }
    }
    public class WeaponInitArgs
    {
        //public UnitAttack UnitAttack { get; set; }
    }


    public abstract class Weapon : MonoBehaviour, IEquipment<Weapon>, IAttackEquipment
    {
        //protected WeaponInitArgs InitArgs;
        [SerializeField] private WeaponUIInfo uiInfo;
        
        public IEquipmentUIInfo UIInfo
        {
            get
            {
                if (!string.IsNullOrEmpty(uiInfo.Title)) return uiInfo;
                uiInfo.Title = GetType().Name;
                return uiInfo;
            }
        }

        public float Power { get; private set; } // ?
        public float Defence { get; private set; } // ?
        public float Level { get; private set; } // ?
        public float Health { get; private set; } // ?

        private static Weapon defaultWeapon;
        private static object lockObj = new();

        public static Weapon Default
        {
            get
            {
                lock (lockObj)
                {
                    if (defaultWeapon)
                        defaultWeapon = new GameObject("NoWeapon").AddComponent<NoWeapon>();
                }
                return defaultWeapon;
            }
        }

        public abstract void Attack();
        public abstract int UnitDamage { get; set; }
        public abstract int BuildingDamage { get; set; }


        public void Modify(Weapon equipment)
        {
            throw new NotImplementedException();
        }
    }

    public class NoWeapon : Weapon
    {
        public override void Attack()
        {
            throw new NotImplementedException();
        }

        public override int UnitDamage { get; set; }
        public override int BuildingDamage { get; set; }
    }
}

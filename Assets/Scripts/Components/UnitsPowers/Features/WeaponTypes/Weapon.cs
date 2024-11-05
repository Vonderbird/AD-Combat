using System;
using RTSEngine.EntityComponent;
using UnityEngine;

namespace ADC
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
        public UnitAttack UnitAttack { get; set; }
    }

    public abstract class Weapon : MonoBehaviour, IEquipment<Weapon>, IAttackEquipment
    {
        protected WeaponInitArgs InitArgs;


        public float Power { get; private set; } // ?
        public float Defence { get; private set; } // ?
        public float Level { get; private set; } // ?
        public float Health { get; private set; } // ?

        public static Weapon Default { get; private set; }

        public void Init(WeaponInitArgs initArgs)
        {
            InitArgs = initArgs;
            Default = new GameObject("NoWeapon").AddComponent<NoWeapon>();
        }
        public abstract void Attack();
        public abstract int UnitDamage { get; set; }
        public abstract int BuildingDamage { get; set; }

        public void Modify(Weapon equipment)
        {
            throw new NotImplementedException();
        }
    }

    public class NoWeapon: Weapon
    {
        public override void Attack()
        {
            throw new NotImplementedException();
        }

        public override int UnitDamage { get; set; }
        public override int BuildingDamage { get; set; }
    }
}

using System;
using RTSEngine.EntityComponent;

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

    public abstract class Weapon : IEquipment, IAttackEquipment
    {
        protected readonly WeaponInitArgs InitArgs;

        protected Weapon(WeaponInitArgs initArgs)
        {
            InitArgs = initArgs;
        }
        public abstract void Attack();
        public abstract int Damage { get; set; }
    }
}

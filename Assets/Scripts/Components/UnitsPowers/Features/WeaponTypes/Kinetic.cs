using UnityEngine;

namespace ADC
{
    public class Kinetic : Weapon
    {
        [SerializeField] private int unitDamage;
        [SerializeField] private int buildingDamage;

        public override int UnitDamage
        {
            get => unitDamage;
            set => unitDamage = value;
        }

        public override int BuildingDamage
        {
            get => buildingDamage;
            set => buildingDamage = value;
        }
        public Kinetic(WeaponInitArgs initArgs) : base(initArgs)
        {
        }

        public override void Attack()
        {

            throw new System.NotImplementedException();
        }

    }
}
using ADC.API;
using UnityEngine;

namespace ADC
{
    public class ExplosiveRounds : Weapon
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

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }

    }
}
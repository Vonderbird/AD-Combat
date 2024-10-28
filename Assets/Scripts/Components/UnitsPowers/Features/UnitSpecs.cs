using System;
using UnityEngine;

namespace ADC
{

    [Serializable]
    public struct UnitSpecs
    {
        [SerializeField] public Armor Armor;
        [SerializeField] public HealthPoint HealthPoint;
        [SerializeField] public ManaPoint ManaPoint;
        [SerializeField] public UnitDamage UnitDamage;
        [SerializeField] public BuildingDamage BuildingDamage;
        
        public void Update(UnitSpecs unitSpecs)
        {
            Armor.Value = unitSpecs.Armor;
            HealthPoint.Value = unitSpecs.HealthPoint;
            ManaPoint.Value = unitSpecs.ManaPoint;
            UnitDamage.Value = unitSpecs.UnitDamage;
            BuildingDamage.Value = unitSpecs.BuildingDamage;
        }

        public static UnitSpecs operator +(UnitSpecs a, UnitSpecs b)
        {
            return new UnitSpecs()
            {
                Armor = new Armor(a.Armor + b.Armor),
                BuildingDamage = new BuildingDamage(a.BuildingDamage + b.BuildingDamage),
                HealthPoint = new HealthPoint(a.HealthPoint + b.HealthPoint),
                ManaPoint = new ManaPoint(a.ManaPoint + b.ManaPoint),
                UnitDamage = new UnitDamage(a.UnitDamage + b.UnitDamage)
            };
        }

        public static UnitSpecs operator -(UnitSpecs a, UnitSpecs b)
        {
            return new UnitSpecs()
            {
                Armor = new Armor(a.Armor - b.Armor),
                BuildingDamage = new BuildingDamage(a.BuildingDamage - b.BuildingDamage),
                HealthPoint = new HealthPoint(a.HealthPoint - b.HealthPoint),
                ManaPoint = new ManaPoint(a.ManaPoint - b.ManaPoint),
                UnitDamage = new UnitDamage(a.UnitDamage - b.UnitDamage)
            };
        }

    }
}
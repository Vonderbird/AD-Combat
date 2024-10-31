using System;
using UnityEngine;

namespace ADC
{

    [Serializable]
    public class UnitSpecs
    {
        [SerializeField] private HealthPoint healthPoint = new();
        [SerializeField] private Armor armor = new();
        [SerializeField] private ManaPoint manaPoint = new();
        [SerializeField] private UnitDamage unitDamage = new();
        [SerializeField] private BuildingDamage buildingDamage  = new();

        public HealthPoint HealthPoint
        {
            get => healthPoint;
            set => healthPoint.Value = value.Value;
        }

        public Armor Armor
        {
            get => armor;
            set => armor.Value = value.Value;
        }

        public ManaPoint ManaPoint
        {
            get => manaPoint;
            set => manaPoint.Value = value.Value;
        }

        public UnitDamage UnitDamage
        {
            get => unitDamage;
            set => unitDamage.Value = value.Value;
        }
        public BuildingDamage BuildingDamage
        {
            get => buildingDamage;
            set => buildingDamage.Value = value.Value;
        }
        
        public void Update(UnitSpecs unitSpecs)
        {
            Armor = unitSpecs.Armor;
            HealthPoint= unitSpecs.HealthPoint;
            ManaPoint= unitSpecs.ManaPoint;
            UnitDamage = unitSpecs.UnitDamage;
            BuildingDamage= unitSpecs.BuildingDamage;
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

        public void SetHandler(Action<object, Armor> handler)
        {
            armor.Changed += handler.Invoke;
        }
        public void SetHandler(Action<object, HealthPoint> handler)
        {
            healthPoint.Changed += handler.Invoke;
        }
        public void SetHandler(Action<object, ManaPoint> handler)
        {
            manaPoint.Changed += handler.Invoke;
        }
        public void SetHandler(Action<object, UnitDamage> handler)
        {
            unitDamage.Changed += handler.Invoke;
        }
        public void SetHandler(Action<object, BuildingDamage> handler)
        {
            buildingDamage.Changed += handler.Invoke;
        }
    }
}
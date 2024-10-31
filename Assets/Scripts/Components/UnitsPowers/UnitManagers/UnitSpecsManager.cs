using System.Reflection;
using UnityEngine;


namespace ADC
{
    public class UnitSpecsManager
    {
        protected UnitSpecs baseSpecs = new();
        protected UnitSpecs equipmentSpecs = new();
        private IThirdPartyInteractionManager thirdPartyManager;

        public UnitSpecs BaseSpecs => baseSpecs;

        public UnitSpecs CurrentSpecs { get; } = new();

        public UnitSpecsManager(IThirdPartyInteractionManager thirdPartyManager)
        {
            Debug.Log("Begin Unit Specs Manager");
            this.thirdPartyManager = thirdPartyManager;
            CurrentSpecs.SetHandler(OnArmorChanged);
            CurrentSpecs.SetHandler(OnHealthPointChanged);
            CurrentSpecs.SetHandler(OnBuildingDamageChanged);
            CurrentSpecs.SetHandler(OnUnitDamageChanged);
            CurrentSpecs.SetHandler(OnChanged);
        }

        public void UpdateBaseSpecs(UnitSpecs baseSpecs)
        {
            this.BaseSpecs.Update(baseSpecs);
            CurrentSpecs.Update(equipmentSpecs + baseSpecs);
        }

        public void BindEquipmentSpecs(UnitSpecs equipmentSpecs)
        {
            equipmentSpecs.SetHandler((object o, Armor a) =>
            {
                this.equipmentSpecs.Armor = a;
                CurrentSpecs.Armor = a + BaseSpecs.Armor;
            });
            equipmentSpecs.SetHandler((object o, HealthPoint a) =>
            {
                this.equipmentSpecs.HealthPoint = a;
                CurrentSpecs.HealthPoint = a + BaseSpecs.HealthPoint;
            });
            equipmentSpecs.SetHandler((object o, BuildingDamage a) =>
            {
                this.equipmentSpecs.BuildingDamage = a;
                CurrentSpecs.BuildingDamage = a + BaseSpecs.BuildingDamage;
            });
            equipmentSpecs.SetHandler((object o, UnitDamage a) =>
            {
                this.equipmentSpecs.UnitDamage = a;
                CurrentSpecs.UnitDamage = a + BaseSpecs.UnitDamage;
            });
            equipmentSpecs.SetHandler((object o, ManaPoint a) =>
            {
                this.equipmentSpecs.ManaPoint = a;
                CurrentSpecs.ManaPoint = a + BaseSpecs.ManaPoint;
            });
        }

        private void OnArmorChanged(object sender, Armor e)
        {
            thirdPartyManager.SetUnitArmor(e.Value);
        }

        private void OnHealthPointChanged(object sender, HealthPoint e)
        {
            thirdPartyManager.SetUnitMaxHealth(e.Value);
        }

        private void OnUnitDamageChanged(object sender, UnitDamage e)
        {
            thirdPartyManager.SetUnitDamage(e.Value);
        }

        private void OnBuildingDamageChanged(object sender, BuildingDamage e)
        {
            thirdPartyManager.SetBuildingDamage(e.Value);
        }

        private void OnChanged(object sender, ManaPoint e)
        {
            thirdPartyManager.SetManaPoint(e.Value);
        }
        public void Heal(int value)
        {
            Debug.LogError("Not Implemented");
        }
        public void ApplyBuff<T>(T buffAmount, float duration) // where T : IUnitFeature<int, T>
        {
            Debug.LogError("Not Implemented");
        }
    }
}
using System.Reflection;
using UnityEngine;


namespace ADC
{
    public class UnitSpecsManager
    {
        protected UnitSpecs baseSpecs = new UnitSpecs();
        protected UnitSpecs currentSpecs = new UnitSpecs();
        protected UnitSpecs equipmentSpecs = new UnitSpecs();
        private IThirdPartyInteractionManager thirdPartyManager;

        public UnitSpecs BaseSpecs => baseSpecs;
        public UnitSpecs CurrentSpecs => currentSpecs;

        public UnitSpecsManager(IThirdPartyInteractionManager thirdPartyManager)
        {
            Debug.Log("Begin Unit Specs Manager");
            this.thirdPartyManager = thirdPartyManager;
            CurrentSpecs.HealthPoint.Changed += OnHealthPointChanged;
            currentSpecs.Armor.Changed += OnArmorChanged;
            CurrentSpecs.BuildingDamage.Changed += OnBuildingDamageChanged;
            CurrentSpecs.UnitDamage.Changed += OnUnitDamageChanged;
            CurrentSpecs.ManaPoint.Changed += OnChanged;
        }

        public void UpdateBaseSpecs(UnitSpecs baseSpecs)
        {
            this.BaseSpecs.Update(baseSpecs);
            CurrentSpecs.Update(equipmentSpecs + baseSpecs);
        }

        public void BindEquipmentSpecs(UnitSpecs equipmentSpecs)
        {
            equipmentSpecs.Armor.Changed += (o, a) =>
            {
                this.equipmentSpecs.Armor.Value = a;
                CurrentSpecs.Armor.Value = a + BaseSpecs.Armor;
            };
            equipmentSpecs.HealthPoint.Changed += (o, a) =>
            {
                this.equipmentSpecs.HealthPoint.Value = a;
                CurrentSpecs.HealthPoint.Value = a + BaseSpecs.HealthPoint;
            };
            equipmentSpecs.BuildingDamage.Changed += (o, a) =>
            {
                this.equipmentSpecs.BuildingDamage.Value = a;
                CurrentSpecs.BuildingDamage.Value = a + BaseSpecs.BuildingDamage;
            };
            equipmentSpecs.UnitDamage.Changed += (o, a) =>
            {
                this.equipmentSpecs.UnitDamage.Value = a;
                CurrentSpecs.UnitDamage.Value = a + BaseSpecs.UnitDamage;
            };
            equipmentSpecs.ManaPoint.Changed += (o, a) =>
            {
                this.equipmentSpecs.ManaPoint.Value = a;
                CurrentSpecs.ManaPoint.Value = a + BaseSpecs.ManaPoint;
            };
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
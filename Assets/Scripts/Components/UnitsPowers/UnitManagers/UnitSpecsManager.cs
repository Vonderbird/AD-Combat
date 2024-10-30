using System.Reflection;
using UnityEngine;


namespace ADC
{
    public class UnitSpecsManager
    {
        protected UnitSpecs baseSpecs;
        protected UnitSpecs currentSpecs;
        protected UnitSpecs additiveSpecs;
        private IThirdPartyInteractionManager thirdPartyManager;

        public UnitSpecs BaseSpecs => baseSpecs;
        public UnitSpecs CurrentSpecs => currentSpecs;

        public UnitSpecsManager(IThirdPartyInteractionManager thirdPartyManager)
        {
            Debug.Log("Begin Unit Specs Manager");
            this.thirdPartyManager = thirdPartyManager; 
            currentSpecs.Initialize();
            currentSpecs.Armor.Changed += OnChanged;
            CurrentSpecs.HealthPoint.Changed += OnChanged;
            CurrentSpecs.BuildingDamage.Changed += OnChanged;
            CurrentSpecs.UnitDamage.Changed += OnChanged;
            CurrentSpecs.ManaPoint.Changed += OnChanged;
        }

        public void UpdateBaseSpecs(UnitSpecs baseSpecs)
        {
            this.BaseSpecs.Update(baseSpecs);
            CurrentSpecs.Update(additiveSpecs + baseSpecs);
        }

        public void UpdateAdditiveSpecs(UnitSpecs additiveSpecs)
        {
            this.additiveSpecs.Update(additiveSpecs);
            CurrentSpecs.Update(additiveSpecs + BaseSpecs);
        }

        private void OnChanged(object sender, Armor e)
        {
            thirdPartyManager.SetUnitArmor(e.Value);
        }

        private void OnChanged(object sender, HealthPoint e)
        {
            thirdPartyManager.SetUnitMaxHealth(e.Value);
        }

        private void OnChanged(object sender, UnitDamage e)
        {
            thirdPartyManager.SetUnitDamage(e.Value);
        }

        private void OnChanged(object sender, BuildingDamage e)
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
        public void ApplyBuff<T>(T buffAmount, float duration) where T : IUnitFeature<int, T>
        {
            Debug.LogError("Not Implemented");
        }
    }
}
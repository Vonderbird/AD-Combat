using System.Reflection;
using UnityEngine;


namespace ADC
{
    public class UnitSpecsManager
    {
        protected UnitSpecs currentSpecs;
        private IThirdPartyInteractionManager thirdPartyManager;

        public UnitSpecsManager(IThirdPartyInteractionManager thirdPartyManager)
        {
            Debug.Log("Begin Unit Specs Manager");
            this.thirdPartyManager = thirdPartyManager;

            currentSpecs.Armor.Changed += OnChanged;
            currentSpecs.HealthPoint.Changed += OnChanged;
            currentSpecs.BuildingDamage.Changed += OnChanged;
            currentSpecs.UnitDamage.Changed += OnChanged;
            currentSpecs.ManaPoint.Changed += OnChanged;
        }

        public void UpdateSpecs(UnitSpecs unitSpecs)
        {
            currentSpecs.Update(unitSpecs);
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
    }
}
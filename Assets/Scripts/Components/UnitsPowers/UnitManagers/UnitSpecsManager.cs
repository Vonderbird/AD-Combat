using ADC.API;
using UnityEngine;


namespace ADC
{
    public class UnitSpecsManager: IUnitSpecsManager
    {
        protected UnitSpecs baseSpecs = new();
        protected UnitSpecs equipmentSpecs = new();
        private IThirdPartyInteractionManager thirdPartyManager;

        public UnitSpecs BaseSpecs => baseSpecs;

        public UnitSpecs CurrentSpecs { get; } = new();

        #region Bind Value Inner Changes
        public UnitSpecsManager(IThirdPartyInteractionManager thirdPartyManager)
        {
            this.thirdPartyManager = thirdPartyManager;
            CurrentSpecs.SetHandler(OnArmorChanged);
            CurrentSpecs.SetHandler(OnHealthPointChanged);
            CurrentSpecs.SetHandler(OnBuildingDamageChanged);
            CurrentSpecs.SetHandler(OnUnitDamageChanged);
            CurrentSpecs.SetHandler(OnManaPointChanged);
        }

        private void OnArmorChanged(object sender, Armor e)
        {
            thirdPartyManager.SetUnitArmor(e.Value);
        }
        private void OnHealthPointChanged(object sender, HealthPoint e)
        {
            thirdPartyManager.SetUnitMaxHealth(e.Value);
        }
        private void OnBuildingDamageChanged(object sender, BuildingDamage e)
        {
            thirdPartyManager.SetBuildingDamage(e.Value);
        }
        private void OnUnitDamageChanged(object sender, UnitDamage e)
        {
            thirdPartyManager.SetUnitDamage(e.Value);
        }
        private void OnManaPointChanged(object sender, ManaPoint e)
        {
            thirdPartyManager.SetManaPoint(e.Value);
        }
        #endregion
        
        public void UpdateBaseSpecs(UnitSpecs baseSpecs)
        {
            this.BaseSpecs.Update(baseSpecs);
            CurrentSpecs.Update(equipmentSpecs + baseSpecs);
        }

        #region Bind Equipment Changes
        public void BindEquipmentSpecs(UnitSpecs equipSpecs)
        {
            equipSpecs.SetHandler(ArmorChangeHandler);
            equipSpecs.SetHandler(HealthPointChangeHandler);
            equipSpecs.SetHandler(BuildingDamageChangeHandler);
            equipSpecs.SetHandler(UnitDamageChangeHandler);
            equipSpecs.SetHandler(ManaPointChangeHandler);
        }
        private void ArmorChangeHandler(object o, Armor a)
        {
            equipmentSpecs.Armor = a;
            CurrentSpecs.Armor = a + BaseSpecs.Armor;
        }
        private void HealthPointChangeHandler(object o, HealthPoint a)
        {
            equipmentSpecs.HealthPoint = a;
            CurrentSpecs.HealthPoint = a + BaseSpecs.HealthPoint;
        }
        private void BuildingDamageChangeHandler(object o, BuildingDamage a)
        {
            equipmentSpecs.BuildingDamage = a;
            CurrentSpecs.BuildingDamage = a + BaseSpecs.BuildingDamage;
        }
        private void UnitDamageChangeHandler(object o, UnitDamage a)
        {
            equipmentSpecs.UnitDamage = a;
            CurrentSpecs.UnitDamage = a + BaseSpecs.UnitDamage;
        }

        private void ManaPointChangeHandler(object o, ManaPoint a)
        {
            equipmentSpecs.ManaPoint = a;
            CurrentSpecs.ManaPoint = a + BaseSpecs.ManaPoint;
        }


        

        #endregion
        
        public void Heal(int value)
        {
            Debug.LogError("Not Implemented");
        }

        public void ApplyBuff(UnitDamage unitDamage, float duration)
        {
            Debug.LogError("Not Implemented");
        }

        public void ApplyBuff(Armor armor, float duration)
        {
            Debug.LogError("Not Implemented");
        }

        public void ApplyBuff(HealthPoint healthPoint, float duration)
        {
            Debug.LogError("Not Implemented");
        }

        public void ApplyBuff(ManaPoint manaPoint, float duration)
        {
            Debug.LogError("Not Implemented");
        }

        public void ApplyBuff<T>(T buffAmount, float duration) where T : IUnitFeature<int, T>
        {
            if (buffAmount is Armor a)
            {
                ApplyBuff(a, duration);
            }
            else if (buffAmount is UnitDamage ud)
            {
                ApplyBuff(ud, duration);
            }
            else if (buffAmount is HealthPoint hp)
            {
                ApplyBuff(hp, duration);
            }
            else if (buffAmount is ManaPoint mp)
            {
                ApplyBuff(mp, duration);
            }
            else
            {
                Debug.LogError("Not Implemented");
            }
        }
    }

}
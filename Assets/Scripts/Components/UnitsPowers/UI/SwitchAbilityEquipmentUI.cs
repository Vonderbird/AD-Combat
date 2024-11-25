using UnityEngine;

namespace ADC
{
    class SwitchAbilityEquipmentUI: MonoBehaviour
    {
        [SerializeField] private GameObject[] equipmentTabs;
        [SerializeField] private GameObject[] abilityTabs;

        public void OnActivateAbilitiesUI()
        {
            DeactivateEquipments();
            for (var i = 0; i < abilityTabs.Length; i++)
            {
                abilityTabs[i].SetActive(true);
            }
        }

        public void OnActivateEquipmentUI()
        {
            DeactivateAbilities();
            for (var i = 0; i < equipmentTabs.Length; i++)
            {
                equipmentTabs[i].SetActive(true);
            }
        }

        private void DeactivateAbilities()
        {
            for (var i = 0; i < abilityTabs.Length; i++)
                abilityTabs[i].SetActive(false);
        }

        private void DeactivateEquipments()
        {
            for (var i = 0; i < equipmentTabs.Length; i++)
                equipmentTabs[i].SetActive(false);
        }
    }
}
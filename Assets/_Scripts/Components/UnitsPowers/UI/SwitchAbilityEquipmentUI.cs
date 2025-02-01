using UnityEngine;
using UnityEngine.UI;

namespace ADC
{
    class SwitchAbilityEquipmentUI: MonoBehaviour
    {
        [SerializeField] private Image abilityButtonImage;
        [SerializeField] private Image equipmentsButtonImage;
        [SerializeField] private GameObject[] equipmentTabs;
        [SerializeField] private GameObject[] abilityTabs;

        public void OnActivateAbilitiesUI()
        {
            DeactivateEquipments();
            for (var i = 0; i < abilityTabs.Length; i++)
            {
                abilityTabs[i].SetActive(true);
            }

            abilityButtonImage.color = new Color(17.0f / 255.0f, 20.0f / 255.0f, 69.0f/255.0f, 1.0f);
        }

        public void OnActivateEquipmentUI()
        {
            DeactivateAbilities();
            for (var i = 0; i < equipmentTabs.Length; i++)
            {
                equipmentTabs[i].SetActive(true);
            }
            equipmentsButtonImage.color = new Color(17.0f / 255.0f, 20.0f / 255.0f, 69.0f / 255.0f, 1.0f);
        }

        private void DeactivateAbilities()
        {
            for (var i = 0; i < abilityTabs.Length; i++)
                abilityTabs[i].SetActive(false);
            abilityButtonImage.color = new Color(17.0f / 255.0f, 20.0f / 255.0f, 69.0f / 255.0f, 0.0f);
        }

        private void DeactivateEquipments()
        {
            for (var i = 0; i < equipmentTabs.Length; i++)
                equipmentTabs[i].SetActive(false);
            equipmentsButtonImage.color = new Color(17.0f / 255.0f, 20.0f / 255.0f, 69.0f / 255.0f, 0.0f);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using RTSEngine.EntityComponent;
using UnityEngine;

namespace ADC.UnitCreation
{

    public class SpawnUnitsList : MonoBehaviour
    {
        [SerializeField] private SpawnUnitActivatorButton activatorButton;

        private List<SpawnUnitActivatorButton> unitButtons = new();
        private SpawnUnitActivatorButton activeButton = null;

        public void AddSpawnUnitUITask(UnitCreationTask task, Action<UnitCreationTask> onActivationClick, float price)
        {
            var unitButton = Instantiate(activatorButton, transform);
            unitButton.SetIconAndText(task.Title, task.Data.icon, price);
            unitButton.SpawnUnitActivated.AddListener(() => onActivationClick(task));
            unitButtons.Add(unitButton);
            unitButton.SpawnUnitActivated.AddListener(() => OnActivateUnit(unitButton));
        }

        private void OnEnable()
        {
            foreach (var t in unitButtons)
            {
                t.SpawnUnitActivated.AddListener(() => OnActivateUnit(t));
            }
        }

        private void OnDisable()
        {
            foreach (var t in unitButtons)
            {
                t.SpawnUnitActivated.RemoveListener(() => OnActivateUnit(t));
            }
        }

        private void OnActivateUnit(SpawnUnitActivatorButton unitButton)
        {
            if (!activeButton || activeButton.Equals(unitButton)) return;
            activeButton?.OnDeactivateButton();
            activeButton = unitButton;
            activeButton?.OnActivateButton();
        }

        public void OnDeactivateAll()
        {
            activeButton?.OnDeactivateButton();
            activeButton = null;
        }

    }
}
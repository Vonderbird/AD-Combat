using System;
using ADC.API;
using UnityEngine;
using RTSEngine.EntityComponent;
using System.Collections.Generic;
using Sisus.Init;

namespace ADC.UnitCreation
{
    public class SpawnUnitsList : MonoBehaviour<IDeactivablesManager>, IDeactivable
    {
        [SerializeField] private SpawnUnitActivatorButton activatorButton;

        /// <summary>
        /// Group Ids, separate multiple group id with ';'
        /// </summary>
        [SerializeField] private string groupId;

        /// <summary>
        /// Ids of groups to deactivate on activation of this button, separate multiple group id with ';'
        /// </summary>
        [SerializeField] private string groupIdsToDeactivate;
        public string[] GroupIds { get; set; }
        private string[] GroupIdsToDeactivate { get; set; }

        private List<SpawnUnitActivatorButton> unitButtons = new();
        private SpawnUnitActivatorButton activeButton = null;
        private Action onDeactivationClick = null;

        protected override void OnAwake()
        {
            GroupIds = groupId.Split(";");
            GroupIdsToDeactivate = groupIdsToDeactivate.Split(';');
            AddToManager();
        }

        public void AddSpawnUnitUITask(UnitCreationTask task, Action<UnitCreationTask> onActivationClick, Action onDeactivationClick, float price)
        {
            var unitButton = Instantiate(activatorButton, transform);
            unitButton.SetIconAndText(task.Title, task.Data.icon, price);
            unitButton.SpawnUnitActivated.AddListener(() => onActivationClick(task));
            unitButtons.Add(unitButton);
            unitButton.SpawnUnitActivated.AddListener(() => OnActivateUnit(unitButton));
            this.onDeactivationClick ??= onDeactivationClick;
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
            if (activeButton && activeButton.Equals(unitButton)) return;

            _deactivablesManager.DeactivateGroups(GroupIdsToDeactivate);
            activeButton?.OnDeactivateButton();
            activeButton = unitButton;
            activeButton?.OnActivateButton();
        }
        
        public void AddToManager()
        {
            foreach (var id in GroupIds)
            {
                _deactivablesManager.Add(id, this);
            }
        }

        public void Deactivate()
        {
            activeButton?.OnDeactivateButton();
            onDeactivationClick?.Invoke();
            activeButton = null;
        }

        private IDeactivablesManager _deactivablesManager;
        protected override void Init(IDeactivablesManager deactivablesManager)
        {
            _deactivablesManager = deactivablesManager;
        }
    }
}
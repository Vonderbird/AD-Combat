using ADC.API;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ADC.UnitCreation
{
    public class DeleteButton : MonoBehaviour, IDeactivable
    {
        private bool isDeleteEnabled = false;


        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image trashIcon;
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

        public bool IsDeleteEnabled => isDeleteEnabled;
        public UnityEvent Clicked;

        private void Awake()
        {
            GroupIds = groupId.Split(';');
            GroupIdsToDeactivate = groupIdsToDeactivate.Split(';');
            AddToManager();
        }

        public void OnDeleteClicked()
        {
            if (!IsDeleteEnabled)
            {
                DeactivablesManager.Instance.DeactivateGroups(GroupIdsToDeactivate);
                Activate();
            }
            else
            {
                Deactivate();
            }

            Clicked?.Invoke();
        }

        public void AddToManager()
        {
            foreach (var id in GroupIds)
            {
                DeactivablesManager.Instance.Add(id, this);
            }
        }

        public void Activate()
        {
            text.color = Color.red;
            trashIcon.color = Color.red;
            isDeleteEnabled = true;
        }

        public void Deactivate()
        {
            text.color = Color.white;
            trashIcon.color = Color.white;
            isDeleteEnabled = false;
        }

    }
}
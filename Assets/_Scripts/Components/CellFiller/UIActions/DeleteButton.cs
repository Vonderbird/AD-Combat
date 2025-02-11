using ADC.API;
using Sisus.Init;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ADC.UnitCreation
{
    public class DeleteButton : MonoBehaviour<IDeactivablesManager>, IDeactivable
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
        private IDeactivablesManager _deactivablesManager;

        private  void Start()
        {
            GroupIds = groupId.Split(';');
            GroupIdsToDeactivate = groupIdsToDeactivate.Split(';');
            AddToManager();
        }
        
        public void OnDeleteClicked()
        {
            if (!IsDeleteEnabled)
            {
                _deactivablesManager.DeactivateGroups(GroupIdsToDeactivate);
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
                _deactivablesManager.Add(id, this);
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

        protected override void Init(IDeactivablesManager deactivablesManager)
        {
            _deactivablesManager = deactivablesManager;
        }
    }
}
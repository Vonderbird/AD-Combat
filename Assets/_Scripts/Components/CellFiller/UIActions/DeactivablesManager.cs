using System.Collections.Generic;
using ADC.API;
using Sisus.Init;
using UnityEngine;

namespace ADC.UnitCreation
{
    [Service(typeof(IDeactivablesManager), FindFromScene = true)]
    public class DeactivablesManager : MonoBehaviour, IDeactivablesManager
    {
        /// <summary>
        /// Group Ids to deactivate on free right click.
        /// Separate each group with ';'.
        /// </summary>
        [SerializeField] private string rcDeactivations;

        private string[] RcDeactivations { get; set; }

        private Dictionary<string, HashSet<IDeactivable>> groupedDeactivables = new();

        private void Awake()
        {
            RcDeactivations = rcDeactivations.Split(';');
        }
        public void Add(string groupId, IDeactivable deactivable)
        {
            if (!groupedDeactivables.ContainsKey(groupId))
                groupedDeactivables.Add(groupId, new HashSet<IDeactivable>());
            groupedDeactivables[groupId].Add(deactivable);
        }

        private void Update()
        {
            if (!Input.GetMouseButtonUp(1)) return;
            foreach (var id in RcDeactivations)
                DeactivateGroup(id);
        }

        public void DeactivateAll()
        {
            foreach (var (groupId, deactivables) in groupedDeactivables)
            {
                foreach (var deactivable in deactivables)
                {
                    deactivable.Deactivate();
                }
            }
        }

        public void DeactivateGroup(string groupId)
        {
            foreach (var deactivable in groupedDeactivables[groupId])
            {
                deactivable.Deactivate();
            }
        }

        public void DeactivateGroups(string[] groupIds)
        {
            foreach (var groupId in groupIds)
            {
                DeactivateGroup(groupId);
            }
        }
    }
}
using ADC.API;
using RTSEngine.Selection;
using Sisus.Init;
using UnityEngine;
using SelectionType = ADC.API.SelectionType;

namespace ADC
{
    public class UnitSelectionCatch : UnitSelectionInfo, IInitializable<IUnitStatsUIPanelManager>
    {
        [SerializeField] private string title = "";
        [SerializeField] private Sprite unitBanner = null;
        private IUnitStatsUIPanelManager _unitStatsUIPanelManager;

        public string Title
        {
            get
            {
                if (!string.IsNullOrEmpty(title)) return title;
                title = GetComponent<IUnitBattleManager>().GetType().Name;
                return title;
            }
        }

        public Sprite UnitBanner => unitBanner;

        public void Init(IUnitStatsUIPanelManager unitStatsUIPanelManager)
        {
            Debug.LogWarning($"unitStatsUIPanelManager: {unitStatsUIPanelManager}");
            _unitStatsUIPanelManager = unitStatsUIPanelManager;
        }

        private void Start()
        {
            var unitSelection = GetComponentInChildren<UnitSelection>(includeInactive:true);
            // if(!unitSelection.gameObject.activeSelf) return;
            unitSelection.Selected += (s, args) => OnUnitSelected(s,
                new SelectionEventArgs((SelectionType)args.Type, s.GetComponent<IUnitBattleManager>()));
            unitSelection.Deselected += (s, args) => OnUnitDeselected(s,
                new DeselectionEventArgs(s.GetComponent<IUnitBattleManager>()));
        }

        public override void OnUnitSelected(object sender, SelectionEventArgs args)
        {
            //if (args.Type == SelectionType.single))
            //{
                var uiInfo = ExtractUnitUIInfo(args.SelectedUnit);
                _unitStatsUIPanelManager.OnUnitSelected(uiInfo);
            //}
        }
        public override void OnUnitDeselected(object sender, DeselectionEventArgs args)
        {
            _unitStatsUIPanelManager.OnUnitDeselected(args.SelectedUnit);
        }

    }
}

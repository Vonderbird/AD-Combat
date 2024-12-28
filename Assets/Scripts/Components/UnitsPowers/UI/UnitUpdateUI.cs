using System;
using System.Collections;
using ADC.API;
using ADC.Currencies;
using RTSEngine.Entities;
using UnityEngine;

namespace ADC
{
    public class UnitUpdateUI : MonoBehaviour
    {
        private UpdateUI activeUpdateUI;
        [SerializeField] private SimpleUpgradeUI SimpleUpgradeUIPrefab;
        [SerializeField] private ModeUpdateUI ModeUpdateUIPrefab;
        
        private bool isDestroying = false;
        private WaitUntil waitForDestroyEnd;
        private void Awake()
        {
            waitForDestroyEnd = new WaitUntil(() => !isDestroying);
        }

        public void RemoveUpdateButton()
        {
            StartCoroutine(DestroyingActiveUI());
        }

        public void OnSelectUnit(IUnitUpdateInfo UpdateInfo, IUnitBattleManager unit)
        {
            if(this.unit == unit) return;
            RemoveUpdateButton();
            this.unit = unit;

            if (UpdateInfo is SimpleUnitUpdateInfo sui)
            {
                AddSimpleUpgradeUI(sui.Cost, sui.OnUpdateAction);
            }
            else if (UpdateInfo is FreeMultiModelUpdateInfo mmi)
            {
                AddModeUpdateUI(mmi.Modes, mmi.ActiveMode, mmi.OnUpdateAction);
            }
        }

        public void AddSimpleUpgradeUI(WarScrap cost, Action onUpdateAction)
        {
            var updateUI = Instantiate(SimpleUpgradeUIPrefab, transform);
            updateUI.UnitUpdateClicked += (o, e) => onUpdateAction?.Invoke();
            activeUpdateUI = updateUI;
        }


        public void AddModeUpdateUI(string[] modesEnum, string activeMode, Action<string> onModeUpdate)
        {
            var updateUI = Instantiate(ModeUpdateUIPrefab, transform);
            var interactable = !unit.GetComponent<Unit>().IsInteractable;
            foreach (var mode in modesEnum)
            {
                updateUI.AddMode(mode, activeMode == mode, interactable);
            }
            updateUI.ModeChanged += (o,e) => onModeUpdate?.Invoke(e);
            activeUpdateUI = updateUI;

            var uiDrawer = ModeUpdateUIPrefab.GetComponentInChildren<UIDrawer>();
            if (uiDrawer == null) return;
            uiDrawer.OnDeactivate();
        }

        private IUnitBattleManager unit;
         
        IEnumerator DestroyingActiveUI()
        {
            if (isDestroying) yield return waitForDestroyEnd;
            if (!activeUpdateUI) yield break;
            isDestroying = true;
            var delItem = activeUpdateUI.gameObject;
            Destroy(delItem);
            yield return new WaitUntil(() => delItem == null);
            isDestroying = false;

        }

        public void OnUnitStateClosing()
        {
            if (activeUpdateUI == null) return;
            if (activeUpdateUI is not ModeUpdateUI mdu) return;
            var uiDrawer = mdu.GetComponentInChildren<UIDrawer>();
            if (uiDrawer == null) return;
            uiDrawer.OnDeactivate();
        }
    }
}

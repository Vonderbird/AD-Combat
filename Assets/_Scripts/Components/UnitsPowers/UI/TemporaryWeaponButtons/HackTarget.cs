using ADC.API;
using RTSEngine.EntityComponent;
using UnityEngine;

namespace ADC
{ 
    public class HackTarget : MonoBehaviour
    {
        public IUnitBattleManager SelectedUnit { get; set; }
        public void OnHackClicked()
        {
            if(SelectedUnit is DeepwalkerInfilterator dwi)
            {
                var attackObject = dwi.transform.GetComponentInChildren<UnitAttack>();
                if (attackObject == null)
                {
                    Debug.LogError($"[HackTarget] Can't find the attack object on unit {dwi.name}");
                    return;
                }
                //attackObject.Set
            }
            // Set it on attack mode deactivate attack component and play an animation
            // Activate attack with settings that convey hack state
            //
        }
    }
}

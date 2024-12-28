using UnityEngine;
using UnityEngine.Events;

public class SwitchCamera : MonoBehaviour
{
    public GameObject BattleCamera;
    public GameObject HomeCamera;
    public int Manager;

    [SerializeField] private UnityEvent BattleCameraEnabled;
    [SerializeField] private UnityEvent HomeCameraEnabled;


    public void ChangeCamera()
    {
        GetComponent<Animator>().SetTrigger("Change");
    }

    public void ManageCamera()
    {
        if (Manager == 0)
        {
            ActivateHomeCamera();
            Manager = 1;
        }
        else
        {
            ActivateBattleCamera();
            Manager = 0;
        }
    }


    void ActivateBattleCamera()
    {
        BattleCamera.SetActive(true);
        HomeCamera.SetActive(false);
        BattleCameraEnabled?.Invoke();
    }

    void ActivateHomeCamera()
    {
        BattleCamera.SetActive(false);
        HomeCamera.SetActive(true);
        HomeCameraEnabled?.Invoke();
    }
}

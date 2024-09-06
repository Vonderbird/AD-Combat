using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject Camera_0;
    public GameObject Camera_1;
    public int Manager;


    public void ChangeCamera()
    {
        GetComponent<Animator>().SetTrigger("Change");
    }

    public void ManageCamera()
    {
        if (Manager == 0)
        {
            Cam_1();
            Manager = 1;
        }
        else
        {
            Cam_0();
            Manager = 0;
        }
    }


    void Cam_0()
    {
        Camera_0.SetActive(true);
        Camera_1.SetActive(false);

    }

    void Cam_1()
    {
        Camera_0.SetActive(false);
        Camera_1.SetActive(true);
    }
}

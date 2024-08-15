using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteButtonController : MonoBehaviour
{
    public Button deleteButton;

    void Start()
    {
        deleteButton.onClick.AddListener(ToggleDeleteMode);
    }

    void ToggleDeleteMode()
    {
        MaterialHoverSwitch.deleteMode = !MaterialHoverSwitch.deleteMode;
    }
}
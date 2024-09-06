using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public class DeleteButtonController : MonoBehaviour
{
    public Button deleteButton;
    private bool deleteModeActive = false;

    private Image deleteButtonImage;

    void Start()
    {
        deleteButton.onClick.AddListener(ToggleDeleteMode);
        deleteButtonImage = deleteButton.GetComponent<Image>();
    }

    void ToggleDeleteMode()
    {
        deleteModeActive = !deleteModeActive;
        MaterialHoverSwitch.deleteMode = deleteModeActive;

        // Change the color of the delete button's image to red when delete mode is active, and back to normal when it's not
        deleteButtonImage.color = deleteModeActive ? Color.red : Color.white;
    }

    public void ResetDeleteButton()
    {
        deleteModeActive = false;
        MaterialHoverSwitch.deleteMode = false;

        // Reset the delete button color to normal
        deleteButtonImage.color = Color.white;
    }
}


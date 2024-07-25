using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnButtonHoverController : MonoBehaviour
{
    public static GameObject selectedUnitPrefab;  // Currently selected unit for spawning
    public GameObject unitPrefab;                 // This button's specific unit prefab
    private Button button;
    private static Button activeButton = null;    // Static reference to the currently active button

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectUnit);
    }

    private void SelectUnit()
    {
        if (selectedUnitPrefab != unitPrefab)
        {
            selectedUnitPrefab = unitPrefab;  // Select this button's unit for spawning
            UpdateButtonColors();
        }
        else  // Toggle off if the same button is clicked again
        {
            selectedUnitPrefab = null;
            UpdateButtonColors();
        }
    }

    private void UpdateButtonColors()
    {
        if (selectedUnitPrefab == unitPrefab)
        {
            if (activeButton != null)
            {
                activeButton.image.color = Color.white;  // Reset previous button color
            }
            button.image.color = Color.green;  // This button goes green
            activeButton = button;  // Update the active button reference
        }
        else
        {
            button.image.color = Color.white;  // Reset this button color if unit is deselected
        }
    }
}

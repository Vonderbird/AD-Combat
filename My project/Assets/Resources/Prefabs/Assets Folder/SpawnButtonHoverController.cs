using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class SpawnButtonHoverController : MonoBehaviour
{
    public static GameObject selectedUnitPrefab;  // Currently selected unit for spawning
    public GameObject unitPrefab;                 // This button's specific unit prefab
    public Vector3 unitScale = Vector3.one;       // Scale of the unit to be spawned
    public int hexagonsToHover = 1;               // Number of hexagons to hover for this button
    private static Button activeButton = null;    // Static reference to the currently active button
    private Button button;

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
            if (activeButton != null)
            {
                activeButton.image.color = Color.white;  // Reset previous button color
            }
            button.image.color = Color.green;  // This button goes green
            activeButton = button;  // Update the active button reference
            MaterialHoverSwitch.SetNumberOfHexagonsToHover(hexagonsToHover);
        }
        else  // Toggle off if the same button is clicked again
        {
            selectedUnitPrefab = null;
            button.image.color = Color.white;  // Reset this button color
            activeButton = null;
            MaterialHoverSwitch.SetNumberOfHexagonsToHover(1); // Reset to single hover
        }
    }

    public static void ResetAllButtons()
    {
        if (activeButton != null)
        {
            activeButton.image.color = Color.white;
            activeButton = null;
        }
        selectedUnitPrefab = null;  // Clear any selected prefab
        MaterialHoverSwitch.SetNumberOfHexagonsToHover(1); // Reset to single hover
    }

    public static Vector3 GetActiveButtonUnitScale()
    {
        if (activeButton != null)
        {
            return activeButton.GetComponent<SpawnButtonHoverController>().unitScale;
        }
        return Vector3.one; // Default scale if no active button is set
    }
}



/*public class SpawnButtonHoverController : MonoBehaviour
{
    public static GameObject selectedUnitPrefab;  // Currently selected unit for spawning
    public GameObject unitPrefab;                 // This button's specific unit prefab
    private static Button activeButton = null;    // Static reference to the currently active button
    private Button button;
    public int hexagonsToHover = 1;               // Number of hexagons to hover for this button

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
            if (activeButton != null)
            {
                activeButton.image.color = Color.white;  // Reset previous button color
            }
            button.image.color = Color.green;  // This button goes green
            activeButton = button;  // Update the active button reference
            MaterialHoverSwitch.SetNumberOfHexagonsToHover(hexagonsToHover);
        }
        else  // Toggle off if the same button is clicked again
        {
            selectedUnitPrefab = null;
            button.image.color = Color.white;  // Reset this button color
            activeButton = null;
            MaterialHoverSwitch.SetNumberOfHexagonsToHover(1); // Reset to single hover
        }
    }

    public static void ResetAllButtons()
    {
        if (activeButton != null)
        {
            activeButton.image.color = Color.white;
            activeButton = null;
        }
        selectedUnitPrefab = null;  // Clear any selected prefab
        MaterialHoverSwitch.SetNumberOfHexagonsToHover(1); // Reset to single hover
    }
}*/
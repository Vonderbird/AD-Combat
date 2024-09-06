using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class SpawnButtonDescription : MonoBehaviour
{
    public DescriptionController descriptionController; // Reference to the DescriptionController
    public GameObject descriptionPanel; // Reference to the Description Panel GameObject
    public string unitDescription; // The description text for the unit

    public void OnClick()
    {
        // Update the description text
        descriptionController.UpdateDescription(unitDescription);

        // Make sure the description panel is visible
        descriptionPanel.SetActive(true);
    }
}

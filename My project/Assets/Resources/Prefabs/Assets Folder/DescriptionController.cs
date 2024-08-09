using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DescriptionController : MonoBehaviour
{
    public TMP_Text descriptionText; // Reference to the TextMeshPro Text component

    public void UpdateDescription(string description)
    {
        descriptionText.text = description;
    }
}
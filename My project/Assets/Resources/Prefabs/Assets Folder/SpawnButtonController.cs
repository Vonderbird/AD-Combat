using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnButtonController : MonoBehaviour
{
    public Color clickedColor = Color.green;
    public Button button;
    public bool isSpawning = false;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnButtonClick()
    {
        isSpawning = true;
        button.image.color = clickedColor;  // This line changes the button's color
    }

    public bool IsSpawning()
    {
        return isSpawning;
    }

    public void ResetSpawning()
    {
        isSpawning = false;
        button.image.color = Color.white; // Optionally reset the button color to white or original color
    }
}

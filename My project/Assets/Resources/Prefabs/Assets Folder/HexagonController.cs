using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class HexagonController : MonoBehaviour
{
    public Color hoverColor = Color.yellow;  // Color when hovered
    private Color originalColor;             // To store the original color
    private Renderer renderer;               // Renderer of the cube

    void Start()
    {
        renderer = GetComponent<Renderer>();         // Get the Renderer component
        originalColor = renderer.material.color;     // Store the original color
    }

    void OnMouseEnter()
    {
        renderer.material.color = hoverColor;  // Change to hover color when the mouse enters
    }

    void OnMouseExit()
    {
        renderer.material.color = originalColor;  // Restore the original color when the mouse exits
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MaterialHoverSwitch : MonoBehaviour
{
    public Material defaultMaterial; // Default material for hexagon
    public Material hoverMaterial;   // Hover material for hexagon
    private Renderer renderer;
    private bool isOccupied = false;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = defaultMaterial; // Set the default material on start
    }

    void OnMouseEnter()
    {
        if (SpawnButtonHoverController.selectedUnitPrefab != null && !isOccupied)
        {
            renderer.material = hoverMaterial; // Change to hover material when the mouse enters
        }
    }

    void OnMouseExit()
    {
        renderer.material = defaultMaterial; // Restore the default material when the mouse exits
    }

    void OnMouseDown()
    {
        if (SpawnButtonHoverController.selectedUnitPrefab != null && !isOccupied)
        {
            // Calculate the exact center position on top of the hexagon
            Vector3 spawnPosition = transform.position + new Vector3(0, 0.6f, 0); // Minor vertical adjustment for visual clarity
            GameObject spawnedUnit = Instantiate(SpawnButtonHoverController.selectedUnitPrefab, spawnPosition, Quaternion.identity);

            // Scale the spawned unit to be 0.7 times the size of the hexagon
            spawnedUnit.transform.localScale = transform.localScale * 0.7f;

            isOccupied = true;
            renderer.material = defaultMaterial;
            SpawnButtonHoverController.ResetAllButtons(); // Reset button colors after spawning
        }
    }
}

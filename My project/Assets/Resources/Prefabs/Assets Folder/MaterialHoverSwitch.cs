using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MaterialHoverSwitch : MonoBehaviour
{
    public Material defaultMaterial; // Default material for hexagon
    public Material hoverMaterial;   // Hover material for hexagon
    public float spawnHeightOffset = 0.5f; // Height offset to ensure no clipping
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
            renderer.material = hoverMaterial;
        }
    }

    void OnMouseExit()
    {
        renderer.material = defaultMaterial;
    }

    void OnMouseDown()
    {
        if (SpawnButtonHoverController.selectedUnitPrefab != null && !isOccupied)
        {
            // Calculate the exact center position on top of the hexagon
            Vector3 spawnPosition = transform.position + new Vector3(0, spawnHeightOffset + renderer.bounds.extents.y, 0);
            GameObject spawnedUnit = Instantiate(SpawnButtonHoverController.selectedUnitPrefab, spawnPosition, Quaternion.identity);

            // Set the size of the spawned unit to be 0.7 times the size of the hexagon
            float scaleFactor = 0.5f; // Desired scale factor relative to the hexagon
            spawnedUnit.transform.localScale = transform.localScale * scaleFactor;

            isOccupied = true;
            renderer.material = defaultMaterial;
            SpawnButtonHoverController.selectedUnitPrefab = null;
        }
    }
}

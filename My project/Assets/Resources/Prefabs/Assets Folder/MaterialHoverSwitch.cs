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
    private static int numberOfHexagonsToHover = 1; // Default to 1 hexagon hover
    private static List<MaterialHoverSwitch> allHexagons = new List<MaterialHoverSwitch>();
    private UnitManager unitManager; // Reference to UnitManager

    void Awake()
    {
        allHexagons.Add(this);
        unitManager = FindObjectOfType<UnitManager>(); // Find the UnitManager in the scene
    }

    void OnDestroy()
    {
        allHexagons.Remove(this);
    }

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
            HighlightAdjacentHexagons(true);
        }
    }

    void OnMouseExit()
    {
        renderer.material = defaultMaterial; // Restore the default material when the mouse exits
        HighlightAdjacentHexagons(false);
    }

    void OnMouseDown()
    {
        if (SpawnButtonHoverController.selectedUnitPrefab != null && !isOccupied)
        {
            Vector3 spawnPosition = CalculateSpawnPosition();
            GameObject spawnedUnit = Instantiate(SpawnButtonHoverController.selectedUnitPrefab, spawnPosition, Quaternion.identity);

            // Scale the spawned unit to be 0.7 times the size of the hexagon
            spawnedUnit.transform.localScale = transform.localScale * 0.7f;

            isOccupied = true;
            renderer.material = defaultMaterial;
            SpawnButtonHoverController.ResetAllButtons(); // Reset button colors after spawning

            // Add the new unit to the UnitManager's list
            unitManager.AddUnit(spawnedUnit, transform.position); // Pass the spawn position for relative positioning
        }
    }

    Vector3 CalculateSpawnPosition()
    {
        float heightOffset = renderer.bounds.extents.y;
        Vector3 spawnPosition = transform.position + new Vector3(0, heightOffset, 0);
        return spawnPosition;
    }

    void HighlightAdjacentHexagons(bool highlight)
    {
        if (numberOfHexagonsToHover == 1)
        {
            // Only highlight this hexagon
            renderer.material = highlight ? hoverMaterial : defaultMaterial;
            return;
        }

        List<MaterialHoverSwitch> adjacentHexagons = GetAdjacentHexagons();
        foreach (var hex in adjacentHexagons)
        {
            if (!hex.isOccupied)
            {
                hex.renderer.material = highlight ? hoverMaterial : defaultMaterial;
                Debug.Log((highlight ? "Highlighting " : "Un-highlighting ") + hex.gameObject.name);
            }
        }
    }

    List<MaterialHoverSwitch> GetAdjacentHexagons()
    {
        List<MaterialHoverSwitch> adjacentHexagons = new List<MaterialHoverSwitch>();

        if (numberOfHexagonsToHover == 3)
        {
            List<MaterialHoverSwitch> potentialAdjacents = new List<MaterialHoverSwitch>();

            foreach (var hex in allHexagons)
            {
                if (hex != this && Vector3.Distance(hex.transform.position, transform.position) < renderer.bounds.size.x * 1.5f)
                {
                    potentialAdjacents.Add(hex);
                }
            }

            // Find a valid triangle pattern
            for (int i = 0; i < potentialAdjacents.Count; i++)
            {
                for (int j = i + 1; j < potentialAdjacents.Count; j++)
                {
                    if (IsValidTriangle(this, potentialAdjacents[i], potentialAdjacents[j]))
                    {
                        adjacentHexagons.Add(potentialAdjacents[i]);
                        adjacentHexagons.Add(potentialAdjacents[j]);
                        return adjacentHexagons;
                    }
                }
            }
        }
        else
        {
            foreach (var hex in allHexagons)
            {
                if (hex != this && Vector3.Distance(hex.transform.position, transform.position) < renderer.bounds.size.x * 1.5f)
                {
                    adjacentHexagons.Add(hex);
                    if (adjacentHexagons.Count >= numberOfHexagonsToHover - 1)
                    {
                        break;
                    }
                }
            }
        }

        Debug.Log("Number of adjacent hexagons to highlight: " + adjacentHexagons.Count);
        return adjacentHexagons;
    }

    bool IsValidTriangle(MaterialHoverSwitch currentHex, MaterialHoverSwitch firstHex, MaterialHoverSwitch secondHex)
    {
        // Check if these hexagons form a triangle (two adjacent hexagons and one below them)
        Vector3 a = currentHex.transform.position;
        Vector3 b = firstHex.transform.position;
        Vector3 c = secondHex.transform.position;

        // Ensure it forms the desired triangle shape (2 next to each other, 1 above)
        bool isNextToEachOther = Mathf.Abs(a.x - b.x) < renderer.bounds.size.x * 1.5f && Mathf.Abs(a.z - b.z) < renderer.bounds.size.z * 0.5f;
        bool isAbove = Mathf.Abs(a.x - c.x) < renderer.bounds.size.x * 0.5f && c.z > a.z && c.z > b.z;

        return isNextToEachOther && isAbove;
    }

    public static void SetNumberOfHexagonsToHover(int number)
    {
        numberOfHexagonsToHover = number;
        Debug.Log("Number of hexagons to hover set to: " + number); // Debug log to check the value being set
    }
}

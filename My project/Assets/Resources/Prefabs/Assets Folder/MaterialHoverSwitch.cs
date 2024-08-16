using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




//delet button added and it changes color
public class MaterialHoverSwitch : MonoBehaviour
{
    public Material defaultMaterial; // Default material for hexagon
    public Material hoverMaterial;   // Hover material for hexagon
    private Renderer renderer;
    private bool isOccupied = false;
    private static int numberOfHexagonsToHover = 1; // Default to 1 hexagon hover
    private static List<MaterialHoverSwitch> allHexagons = new List<MaterialHoverSwitch>();
    private UnitManager unitManager; // Reference to UnitManager
    private GameObject spawnedUnit; // Store the spawned unit for deletion

    public static bool deleteMode = false; // Static flag to toggle delete mode
    private DeleteButtonController deleteButtonController;

    void Awake()
    {
        allHexagons.Add(this);
        unitManager = FindObjectOfType<UnitManager>(); // Find the UnitManager in the scene
        deleteButtonController = FindObjectOfType<DeleteButtonController>(); // Find the DeleteButtonController in the scene
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
        if (SpawnButtonHoverController.selectedUnitPrefab != null && !isOccupied && !deleteMode)
        {
            renderer.material = hoverMaterial; // Change to hover material when the mouse enters
            HighlightAdjacentHexagons(true);
        }
    }

    void OnMouseExit()
    {
        if (!deleteMode)
        {
            renderer.material = defaultMaterial; // Restore the default material when the mouse exits
            HighlightAdjacentHexagons(false);
        }
    }

    void OnMouseDown()
    {
        if (deleteMode && isOccupied && spawnedUnit != null)
        {
            // Delete the unit if delete mode is active
            Destroy(spawnedUnit);
            isOccupied = false;
            renderer.material = defaultMaterial; // Reset material after deletion
            deleteButtonController.ResetDeleteButton(); // Reset delete button after deleting the unit
        }
        else if (!deleteMode && SpawnButtonHoverController.selectedUnitPrefab != null && !isOccupied)
        {
            List<MaterialHoverSwitch> selectedHexagons = GetAdjacentHexagons();
            selectedHexagons.Add(this); // Include the current hexagon

            Vector3 spawnPosition = CalculateAveragePosition(selectedHexagons);
            spawnedUnit = Instantiate(SpawnButtonHoverController.selectedUnitPrefab, spawnPosition, Quaternion.identity);

            // Apply the scale defined in the SpawnButtonHoverController
            spawnedUnit.transform.localScale = SpawnButtonHoverController.GetActiveButtonUnitScale();

            // Mark all selected hexagons as occupied and disable further interactions
            foreach (MaterialHoverSwitch hex in selectedHexagons)
            {
                hex.isOccupied = true;
                hex.renderer.material = defaultMaterial; // Reset material after spawning
            }

            SpawnButtonHoverController.ResetAllButtons(); // Reset button colors after spawning

            // Add the new unit to the UnitManager's list
            unitManager.AddUnit(spawnedUnit, spawnPosition); // Pass the spawn position for relative positioning
        }
    }

    Vector3 CalculateAveragePosition(List<MaterialHoverSwitch> hexagons)
    {
        Vector3 averagePosition = Vector3.zero;
        foreach (MaterialHoverSwitch hex in hexagons)
        {
            averagePosition += hex.transform.position;
        }
        averagePosition /= hexagons.Count;
        return averagePosition;
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

            // Find a valid front-facing triangle pattern (two adjacent hexagons and one above)
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
        else if (numberOfHexagonsToHover == 2)
        {
            foreach (var hex in allHexagons)
            {
                if (hex != this && Vector3.Distance(hex.transform.position, transform.position) < renderer.bounds.size.x * 1.5f)
                {
                    adjacentHexagons.Add(hex);
                    break;
                }
            }
        }

        return adjacentHexagons;
    }

    bool IsValidTriangle(MaterialHoverSwitch currentHex, MaterialHoverSwitch firstHex, MaterialHoverSwitch secondHex)
    {
        // Check if these hexagons form a front-facing triangle (two adjacent hexagons and one above them)
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





//Scalling and positioning in the centre of the hexagons
/*public class MaterialHoverSwitch : MonoBehaviour
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
            List<MaterialHoverSwitch> selectedHexagons = GetAdjacentHexagons();
            selectedHexagons.Add(this); // Include the current hexagon

            Vector3 spawnPosition = CalculateAveragePosition(selectedHexagons);
            GameObject spawnedUnit = Instantiate(SpawnButtonHoverController.selectedUnitPrefab, spawnPosition, Quaternion.identity);

            // Apply the scale defined in the SpawnButtonHoverController
            spawnedUnit.transform.localScale = SpawnButtonHoverController.GetActiveButtonUnitScale();

            // Mark all selected hexagons as occupied and disable further interactions
            foreach (MaterialHoverSwitch hex in selectedHexagons)
            {
                hex.isOccupied = true;
                hex.renderer.material = defaultMaterial; // Reset material after spawning
            }

            SpawnButtonHoverController.ResetAllButtons(); // Reset button colors after spawning

            // Add the new unit to the UnitManager's list
            unitManager.AddUnit(spawnedUnit, spawnPosition); // Pass the spawn position for relative positioning
        }
    }

    Vector3 CalculateAveragePosition(List<MaterialHoverSwitch> hexagons)
    {
        Vector3 averagePosition = Vector3.zero;
        foreach (MaterialHoverSwitch hex in hexagons)
        {
            averagePosition += hex.transform.position;
        }
        averagePosition /= hexagons.Count;
        return averagePosition;
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

            // Find a valid front-facing triangle pattern (two adjacent hexagons and one above)
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
        else if (numberOfHexagonsToHover == 2)
        {
            foreach (var hex in allHexagons)
            {
                if (hex != this && Vector3.Distance(hex.transform.position, transform.position) < renderer.bounds.size.x * 1.5f)
                {
                    adjacentHexagons.Add(hex);
                    break;
                }
            }
        }

        return adjacentHexagons;
    }

    bool IsValidTriangle(MaterialHoverSwitch currentHex, MaterialHoverSwitch firstHex, MaterialHoverSwitch secondHex)
    {
        // Check if these hexagons form a front-facing triangle (two adjacent hexagons and one above them)
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





//Scalling working well
/*public class MaterialHoverSwitch : MonoBehaviour
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

            // Apply the scale defined in the SpawnButtonHoverController
            spawnedUnit.transform.localScale = SpawnButtonHoverController.GetActiveButtonUnitScale();

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
}*/




// The portal action is done with this code
/*public class MaterialHoverSwitch : MonoBehaviour
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
}*/





// This is the code which does the spawning task perfectly but not the portal
/*public class MaterialHoverSwitch : MonoBehaviour
{
    public Material defaultMaterial; // Default material for hexagon
    public Material hoverMaterial;   // Hover material for hexagon
    private Renderer renderer;
    private bool isOccupied = false;
    private static int numberOfHexagonsToHover = 1; // Default to 1 hexagon hover
    private static List<MaterialHoverSwitch> allHexagons = new List<MaterialHoverSwitch>();

    void Awake()
    {
        allHexagons.Add(this);
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

        // Check if distances form a valid triangle
        float ab = Vector3.Distance(a, b);
        float bc = Vector3.Distance(b, c);
        float ca = Vector3.Distance(c, a);

        // Ensure it forms the desired triangle shape (2 next to each other, 1 below)
        return ab < renderer.bounds.size.x * 1.5f && bc < renderer.bounds.size.x * 1.5f && Mathf.Abs(a.y - c.y) < renderer.bounds.size.y * 1.5f && Mathf.Abs(b.y - c.y) < renderer.bounds.size.y * 1.5f;
    }

    public static void SetNumberOfHexagonsToHover(int number)
    {
        numberOfHexagonsToHover = number;
        Debug.Log("Number of hexagons to hover set to: " + number); // Debug log to check the value being set
    }
}*/


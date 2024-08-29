using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RTSEngine.Entities;
using RTSEngine.UnitExtension;





public class UnitManager : MonoBehaviour
{
    public List<GameObject> spawnedUnits = new List<GameObject>(); // List to store the spawned units
    public List<Vector3> relativePositions = new List<Vector3>(); // List to store the relative positions of the units
    public Transform yamatanBase; // Reference to the Yamatan base in the battle ground
    public TMP_Text timerText; // Reference to the TextMeshPro Text for the timer
    private float timer = 30f; // 30 second timer

    void Start()
    {
        // Initialize the timer text
        timerText.text = "Time until move: " + timer.ToString("F0");
        StartCoroutine(MoveUnitsEvery30Seconds());
    }

    void Update()
    {
        // Update the timer
        timer -= Time.deltaTime;
        timerText.text = "Time until move: " + Mathf.Ceil(timer).ToString();

        // Reset timer every 30 seconds
        if (timer <= 0)
        {
            timer = 30f;
        }
    }

    IEnumerator MoveUnitsEvery30Seconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);

            if (spawnedUnits.Count == 0) yield return null;

            Vector3 basePosition = yamatanBase.position + new Vector3(2, 0, -7); // Offset by 10 units in the x direction

            // Calculate the average position of the spawned units in the homeworld
            Vector3 averageHomePosition = Vector3.zero;
            foreach (Vector3 pos in relativePositions)
            {
                averageHomePosition += pos;
            }
            averageHomePosition /= relativePositions.Count;

            // Move each unit to the battleground position, maintaining relative positions
            for (int i = 0; i < spawnedUnits.Count; i++)
            {
                GameObject unit = spawnedUnits[i];
                if (unit != null) // Check if unit exists
                {
                    Vector3 relativePosition = relativePositions[i] - averageHomePosition;
                    Vector3 newPosition = basePosition + relativePosition;
                    unit.transform.position = newPosition;
                    Debug.Log($"Unit {unit.name} moved to {newPosition}");

                    // Trigger the unit to start moving toward the target
                    AutoMoveToTarget movementScript = unit.GetComponent<AutoMoveToTarget>();
                    if (movementScript != null)
                    {
                        movementScript.StartMoving(); // Start the automatic movement
                    }
                }
            }

            Debug.Log("Units moved to battleground");
        }
    }

    // Method to add units to the list when they are spawned
    public void AddUnit(GameObject unit, Vector3 originalPosition)
    {
        spawnedUnits.Add(unit);
        relativePositions.Add(originalPosition); // Store the original position
        Debug.Log($"Unit {unit.name} added at {originalPosition}");
    }
}


//last version
/*public class UnitManager : MonoBehaviour
{
    public List<GameObject> spawnedUnits = new List<GameObject>(); // List to store the spawned units
    public List<Vector3> relativePositions = new List<Vector3>(); // List to store the relative positions of the units
    public Transform yamatanBase; // Reference to the Yamatan base in the battle ground
    public TMP_Text timerText; // Reference to the TextMeshPro Text for the timer
    private float timer = 30f; // 30 second timer

    private GameObject selectedUnit = null; // Reference to the currently selected unit

    void Start()
    {
        // Initialize the timer text
        timerText.text = "Time until move: " + timer.ToString("F0");
        StartCoroutine(MoveUnitsEvery30Seconds());
    }

    void Update()
    {
        // Update the timer
        timer -= Time.deltaTime;
        timerText.text = "Time until move: " + Mathf.Ceil(timer).ToString();

        // Reset timer every 30 seconds
        if (timer <= 0)
        {
            timer = 30f;
        }
    }

    IEnumerator MoveUnitsEvery30Seconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);

            if (spawnedUnits.Count == 0) yield return null;

            Vector3 basePosition = yamatanBase.position + new Vector3(2, 0, -7); // Offset by 10 units in the x direction

            // Calculate the average position of the spawned units in the homeworld
            Vector3 averageHomePosition = Vector3.zero;
            foreach (Vector3 pos in relativePositions)
            {
                averageHomePosition += pos;
            }
            averageHomePosition /= relativePositions.Count;

            // Move each unit to the battle ground position, maintaining relative positions
            for (int i = 0; i < spawnedUnits.Count; i++)
            {
                GameObject unit = spawnedUnits[i];
                if (unit != null) // Check if unit exists
                {
                    Vector3 relativePosition = relativePositions[i] - averageHomePosition;
                    Vector3 newPosition = basePosition + relativePosition;
                    unit.transform.position = newPosition;
                    Debug.Log($"Unit {unit.name} moved to {newPosition}");
                }
            }

            Debug.Log("Units moved to battle ground");
        }
    }

    // Method to add units to the list when they are spawned
    public void AddUnit(GameObject unit, Vector3 originalPosition)
    {
        spawnedUnits.Add(unit);
        relativePositions.Add(originalPosition); // Store the original position
        Debug.Log($"Unit {unit.name} added at {originalPosition}");
    }

    // Method to select a unit for deletion
    public void SelectUnit(GameObject unit)
    {
        selectedUnit = unit;
    }

    // Method to delete the selected unit
    public void DeleteSelectedUnit()
    {
        if (selectedUnit != null)
        {
            spawnedUnits.Remove(selectedUnit);
            Destroy(selectedUnit);
            Debug.Log($"Unit {selectedUnit.name} deleted");
            selectedUnit = null;
        }
    }
}


// The portal action is done with this code
/*public class UnitManager : MonoBehaviour
{
    public List<GameObject> spawnedUnits = new List<GameObject>(); // List to store the spawned units
    public List<Vector3> relativePositions = new List<Vector3>(); // List to store the relative positions of the units
    public Transform yamatanBase; // Reference to the Yamatan base in the battle ground
    public TMP_Text timerText; // Reference to the TextMeshPro Text for the timer
    private float timer = 30f; // 30 second timer

    void Start()
    {
        // Initialize the timer text
        timerText.text = "Time until move: " + timer.ToString("F0");
        StartCoroutine(MoveUnitsEvery30Seconds());
    }

    void Update()
    {
        // Update the timer
        timer -= Time.deltaTime;
        timerText.text = "Time until move: " + Mathf.Ceil(timer).ToString();

        // Reset timer every 30 seconds
        if (timer <= 0)
        {
            timer = 30f;
        }
    }

    IEnumerator MoveUnitsEvery30Seconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);

            if (spawnedUnits.Count == 0) yield return null;

            Vector3 basePosition = yamatanBase.position + new Vector3(2, 0, -7); // Offset by 10 units in the x direction

            // Calculate the average position of the spawned units in the homeworld
            Vector3 averageHomePosition = Vector3.zero;
            foreach (Vector3 pos in relativePositions)
            {
                averageHomePosition += pos;
            }
            averageHomePosition /= relativePositions.Count;

            // Move each unit to the battle ground position, maintaining relative positions
            for (int i = 0; i < spawnedUnits.Count; i++)
            {
                GameObject unit = spawnedUnits[i];
                if (unit != null) // Check if unit exists
                {
                    Vector3 relativePosition = relativePositions[i] - averageHomePosition;
                    Vector3 newPosition = basePosition + relativePosition;
                    unit.transform.position = newPosition;
                    unit.transform.rotation = Quaternion.Euler(0, 180, 0); // Rotate the unit 180 degrees on the Y-axis
                    Debug.Log($"Unit {unit.name} moved to {newPosition}");
                }
            }

            Debug.Log("Units moved to battle ground");
        }
    }

    // Method to add units to the list when they are spawned
    public void AddUnit(GameObject unit, Vector3 originalPosition)
    {
        spawnedUnits.Add(unit);
        relativePositions.Add(originalPosition); // Store the original position
        Debug.Log($"Unit {unit.name} added at {originalPosition}");
    }
}*/






/*public class UnitManager : MonoBehaviour
{
    public List<GameObject> spawnedUnits; // List to store the spawned units
    public Transform battleGroundPosition; // Target position for the units to move to
    public TMP_Text timerText; // Reference to the TextMeshPro Text for the timer
    private float timer = 30f; // 30 second timer

    void Start()
    {
        // Initialize the timer text
        timerText.text = "Time until move: " + timer.ToString("F0");
        StartCoroutine(MoveUnitsEvery30Seconds());
    }

    void Update()
    {
        // Update the timer
        timer -= Time.deltaTime;
        timerText.text = "Time until move: " + Mathf.Ceil(timer).ToString();

        // Reset timer every 30 seconds
        if (timer <= 0)
        {
            timer = 30f;
        }
    }

    IEnumerator MoveUnitsEvery30Seconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);

            // Move each unit to the battle ground position
            foreach (GameObject unit in spawnedUnits)
            {
                if (unit != null) // Check if unit exists
                {
                    unit.transform.position = battleGroundPosition.position;
                }
            }

            Debug.Log("Units moved to battle ground");
        }
    }

    // Method to add units to the list when they are spawned
    public void AddUnit(GameObject unit)
    {
        spawnedUnits.Add(unit);
    }
}*/
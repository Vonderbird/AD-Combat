using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import the TextMeshPro namespace


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

            // Move each unit to the battle ground position
            for (int i = 0; i < spawnedUnits.Count; i++)
            {
                GameObject unit = spawnedUnits[i];
                if (unit != null) // Check if unit exists
                {
                    Vector3 newPosition = yamatanBase.position + relativePositions[i];
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
        relativePositions.Add(originalPosition - unit.transform.position); // Calculate relative position
        Debug.Log($"Unit {unit.name} added at {originalPosition} with relative position {relativePositions[relativePositions.Count - 1]}");
    }
}

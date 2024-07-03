using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnObjectOnClick : MonoBehaviour
{
    public GameObject objectToSpawn;  // Drag the prefab you want to spawn here in the Unity Inspector.
    private Vector3 nextSpawnPosition = new Vector3(44, 1, 70);  // Initial position for the first spawn.
    private int spawnDistance = 2;  // Distance each object will be spawned from the previous one.

    // This public method will be called when the UI button is clicked.
    public void OnButtonClick()
    {
        Debug.Log("Button was clicked.");  // Logs a message to the console to confirm the button press.

        // Instantiate the object at the current spawn position.
        GameObject spawnedObject = Instantiate(objectToSpawn, nextSpawnPosition, Quaternion.identity);

        if (spawnedObject != null)
        {
            Debug.Log("Object spawned at: " + spawnedObject.transform.position);  // Logs the position where the object was spawned
            // Update the position for the next spawn.
            nextSpawnPosition.x += spawnDistance;
        }
        else
        {
            Debug.LogError("Failed to spawn object.");  // Logs an error if the object could not be spawned
        }
    }
}
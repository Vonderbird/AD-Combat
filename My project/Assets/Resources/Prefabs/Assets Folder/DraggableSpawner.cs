using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Assign this in the Unity Inspector
    public Camera gameCamera; // Assign the specific camera in the Unity Inspector

    // Method to be called by the UI button
    public void SpawnObject()
    {
        // Log the mouse position in screen coordinates
        Debug.Log("Mouse Screen Position: " + Input.mousePosition);

        // Convert screen position to world position using the specific camera
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10); // '10' should be adjusted based on the distance from the camera to the plane of interaction
        Vector3 worldPosition = gameCamera.ScreenToWorldPoint(screenPosition);

        // Log the converted world position
        Debug.Log("World Position: " + worldPosition);

        // Instantiate the object at the calculated world position
        GameObject spawnedObject = Instantiate(objectToSpawn, worldPosition, Quaternion.identity);
        if (spawnedObject != null)
        {
            Debug.Log("Object spawned at: " + spawnedObject.transform.position);
        }
        else
        {
            Debug.LogError("Failed to spawn object.");
        }
    }
}

// This is a simple draggable component you can expand upon
public class Draggable : MonoBehaviour
{
    public Camera gameCamera; // Assign the specific camera in the Unity Inspector
    private bool dragging = false;

    void Update()
    {
        if (dragging)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10); // Adjust '10' to match the camera's settings
            transform.position = gameCamera.ScreenToWorldPoint(position);
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
    }
}
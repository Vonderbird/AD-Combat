using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class HexagonController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private Color originalColor;
    private Renderer renderer;
    public GameObject unitPrefab;  // Assign this via the inspector
    public SpawnButtonController spawnButtonController;  // Assign this via the inspector

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (spawnButtonController.IsSpawning())
        {
            renderer.material.color = Color.yellow;  // Highlight color
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (spawnButtonController.IsSpawning())
        {
            Instantiate(unitPrefab, transform.position, Quaternion.identity);  // Spawn the unit
            spawnButtonController.ResetSpawning();  // Reset the spawning state
            renderer.material.color = originalColor;  // Reset hexagon color
        }
    }
}
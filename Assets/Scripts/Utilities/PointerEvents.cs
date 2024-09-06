using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class PointerEvents<T> : Singleton<PointerEvents<T>> where T : PointerEvents<T>
{
    [SerializeField]
    private LayerMask customLayerMask; // The custom layer mask for raycast

    [SerializeField] private Camera camera;

    private bool isPointerOver = false;
    private readonly PointerEventArgs pointerEventArgs = new();

    public UnityEvent<PointerEventArgs> PointerEntered;
    public UnityEvent<PointerEventArgs> PointerExited;
    public UnityEvent<PointerEventArgs> PointerClicked;

    private GameObject lastHitObject;

    void Awake()
    {
        StartCoroutine(FrameUpdate());
    }

    IEnumerator FrameUpdate()
    {
        while (true)
        {
            // use PointerOverCustomLayer 
            PointerOverCustomLayer(Input.mousePosition);

            // Check for click events
            if (isPointerOver && Input.GetMouseButtonDown(0)) // Left mouse button
            {
                PointerClicked?.Invoke(pointerEventArgs);
            }

            yield return null;
        }
    }

    // Custom method to check if the pointer is over the specified layer
    private void PointerOverCustomLayer(Vector3 pointerScreenPosition)
    {
        if(!camera.gameObject.activeSelf) return;
        // Perform a raycast from the camera to the pointer position
        Ray ray = camera.ScreenPointToRay(pointerScreenPosition);
        RaycastHit hit;

        // Check if the raycast hits an object with the specified layer mask
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, customLayerMask))
        {
            if (hit.collider.gameObject != lastHitObject)
            {
                if (lastHitObject != null)
                {
                    // Trigger PointerExited for the last hit object
                    PointerExited?.Invoke(pointerEventArgs);
                }

                pointerEventArgs.Point = hit.point;
                pointerEventArgs.Normal = hit.normal;
                pointerEventArgs.HitObject = hit.collider.gameObject;
                // Update the last hit object
                lastHitObject = hit.collider.gameObject;

                // Trigger PointerEntered for the current hit object
                isPointerOver = true;
                PointerEntered?.Invoke(pointerEventArgs);
            }
        }
        else
        {
            if (isPointerOver && lastHitObject != null)
            {
                // Trigger PointerExited if the pointer was previously over an object
                PointerExited?.Invoke(pointerEventArgs);
                isPointerOver = false;
                lastHitObject = null;
                pointerEventArgs.HitObject = null;
            }
        }
    }

    // Optional: Visualize the raycast in the editor for debugging purposes
    private void OnDrawGizmos()
    {
        if (isPointerOver)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
}


public class PointerEventArgs
{
    public Vector3 Point { get; set; }
    public Vector3 Normal { get; set; }
    public GameObject HitObject { get; set; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AutoMoveToTarget : MonoBehaviour
{
    [SerializeField]
    private Transform target; // The target to move toward (e.g., enemy base)
    [SerializeField]
    private float speed = 5f; // The speed at which the unit moves

    private bool shouldMove = false;

    void Update()
    {
        if (shouldMove && target != null)
        {
            MoveTowardsTarget();
        }
    }

    public void StartMoving()
    {
        shouldMove = true;
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Optional: Stop moving when the unit reaches the target
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            shouldMove = false;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
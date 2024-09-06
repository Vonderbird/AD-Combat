using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TeleportUnits : MonoBehaviour
{
    public Transform homeworld; // Reference to the Homeworld
    public Transform battleground; // Reference to the Battleground
    public float teleportInterval = 30f; // Time in seconds between each teleportation

    void Start()
    {
        StartCoroutine(TeleportUnitsEveryInterval());
    }

    IEnumerator TeleportUnitsEveryInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(teleportInterval);
            MoveUnitsToBattleground();
        }
    }

    void MoveUnitsToBattleground()
    {
        Debug.Log("Teleporting units...");

        // Iterate through all units in the homeworld
        foreach (Transform unit in homeworld)
        {
            // Move the unit to the exact position of the battleground
            unit.position = battleground.position;
            unit.rotation = battleground.rotation;
            unit.localScale = battleground.localScale;

            // Reparent the unit to the battleground
            unit.SetParent(battleground);
        }

        Debug.Log("Units teleported.");
    }
}

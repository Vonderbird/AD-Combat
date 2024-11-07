using ADC.API;
using UnityEngine;

namespace ADC
{
    public class CarbonCompound : Shield
    {
        public override void Defend()
        {
            // Basic example of reducing incoming damage based on armor
            Debug.Log($"Defending with {Armor} armor");
            // Implement specific defense behavior, like reducing damage
        }

    }
}
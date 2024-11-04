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

        public override void Defend(Biological weapon)
        {
            throw new System.NotImplementedException();
        }

        public override void Defend(Electromagnetic weapon)
        {
            throw new System.NotImplementedException();
        }

        public override void Defend(ExplosiveRounds weapon)
        {
            throw new System.NotImplementedException();
        }

        public override void Defend(Sharpened weapon)
        {
            throw new System.NotImplementedException();
        }

        public override void Defend(Kinetic weapon)
        {
            throw new System.NotImplementedException();
        }

        public override void Defend(Plasma weapon)
        {
            throw new System.NotImplementedException();
        }

    }
}
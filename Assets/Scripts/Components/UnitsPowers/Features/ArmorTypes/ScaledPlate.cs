using UnityEngine;

namespace ADC
{
    public class ScaledPlate : Shield
    {
        [SerializeField] private int armor;
        public override int Armor => armor;
        public override void Defend()
        {
            throw new System.NotImplementedException();
        }

        public ScaledPlate(ShieldInitArgs args) : base(args)
        {
        }
    }
}
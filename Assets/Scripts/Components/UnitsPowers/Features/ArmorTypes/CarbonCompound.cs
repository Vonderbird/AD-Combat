using UnityEngine;

namespace ADC
{
    public class CarbonCompound : Shield
    {
        [SerializeField] private int armor;
        public override int Armor => armor;

        public override void Defend()
        {
            throw new System.NotImplementedException();
        }


        public CarbonCompound(ShieldInitArgs args) : base(args)
        {
        }
    }
}
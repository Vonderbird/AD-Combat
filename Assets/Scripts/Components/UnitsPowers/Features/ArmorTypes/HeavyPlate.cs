using UnityEngine;

namespace ADC
{
    public class HeavyPlate : Shield
    {
        [SerializeField] private int armor;
        public override int Armor => armor;
        public override void Defend()
        {
            throw new System.NotImplementedException();
        }

        public HeavyPlate(ShieldInitArgs args) : base(args)
        {
        }
    }
}
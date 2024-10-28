using UnityEngine;

namespace ADC
{
    public class Nano : Shield
    {
        [SerializeField] private int armor;
        public override int Armor => armor;
        public override void Defend()
        {
            throw new System.NotImplementedException();
        }

        public Nano(ShieldInitArgs args) : base(args)
        {
        }
    }
}
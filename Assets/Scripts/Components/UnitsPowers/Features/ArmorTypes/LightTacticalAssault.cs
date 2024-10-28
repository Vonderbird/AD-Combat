using UnityEngine;

namespace ADC
{
    public class LightTacticalAssault : Shield
    {
        [SerializeField] private int armor;
        public override int Armor => armor;
        public override void Defend()
        {
            throw new System.NotImplementedException();
        }

        public LightTacticalAssault(ShieldInitArgs args) : base(args)
        {
        }
    }
}
using System;
using RTSEngine.Health;

namespace ADC
{
    public class ShieldEventArgs : EventArgs
    {
        public Shield Shield { get; set; }
    }

    public class ShieldInitArgs
    {
        public UnitHealth UnitHealth { get; set; }
    }

    public abstract class Shield: IEquipment, IProtectorEquipment
    {
        private readonly ShieldInitArgs args;

        protected Shield(ShieldInitArgs args)
        {
            this.args = args;
        }

        public abstract void Defend();
        public abstract int Armor { get; }
    }
}

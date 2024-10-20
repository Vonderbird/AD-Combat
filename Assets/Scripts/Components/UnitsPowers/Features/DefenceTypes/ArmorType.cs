using System;
using RTSEngine.Health;

namespace ADC
{
    public class ArmorTypeEventArgs : EventArgs
    {
        public ArmorType ArmorType { get; set; }
    }

    public class ArmorTypeInitArgs
    {
        public UnitHealth UnitHealth { get; set; }
    }

    public abstract class ArmorType
    {
        private readonly ArmorTypeInitArgs args;

        protected ArmorType(ArmorTypeInitArgs args)
        {
            this.args = args;
        }

        public abstract void Defend();
    }
}

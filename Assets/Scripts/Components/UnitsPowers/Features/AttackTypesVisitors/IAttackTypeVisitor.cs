using UnityEngine;

namespace ADC
{
    public interface IAttackTypeVisitor
    {
        void Visit(Plasma attackType);
        void Visit(Sharpened attackType);
        void Visit(ExplosiveRound attackType);
        void Visit(Biological attackType);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADC
{
    public interface ISpecialAbility
    {
        void UnLock();
    }

    public class AdvancingThePathway : ISpecialAbility
    {
        public void UnLock()
        {
            throw new System.NotImplementedException();
        }
    }
    public class Tempest : ISpecialAbility
    {
        public void UnLock()
        {
            throw new System.NotImplementedException();
        }
    }
    public class FlameWalker : ISpecialAbility
    {
        public void UnLock()
        {
            throw new System.NotImplementedException();
        }
    }
}

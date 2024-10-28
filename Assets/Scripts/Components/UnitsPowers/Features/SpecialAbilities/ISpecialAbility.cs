using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADC
{
    public interface ISpecialAbility
    {
        event EventHandler UnLocked;
        void Use();
        void OnLevelChanged(object sender, Level e);
    }

    public class AdvancingThePathway : ISpecialAbility
    {
        public event EventHandler UnLocked;

        public void Use()
        {
            throw new System.NotImplementedException();
        }

        public void OnLevelChanged(object sender, Level e)
        {
            throw new NotImplementedException();
        }
    }
    public class Tempest : ISpecialAbility
    {
        public event EventHandler UnLocked;

        public void Use()
        {
            throw new System.NotImplementedException();
        }

        public void OnLevelChanged(object sender, Level e)
        {
            throw new NotImplementedException();
        }
    }
    public class FlameWalker : ISpecialAbility
    {
        public event EventHandler UnLocked;

        public void Use()
        {
            throw new System.NotImplementedException();
        }

        public void OnLevelChanged(object sender, Level e)
        {
            throw new NotImplementedException();
        }
    }
}

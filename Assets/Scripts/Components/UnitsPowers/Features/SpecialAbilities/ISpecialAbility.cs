using System;
using UnityEngine;

namespace ADC
{
    public interface ISpecialAbility
    {
        Level Level { get; }
        event EventHandler UnLocked;
        void Use();
        void OnLevelChanged(object sender, Level e);
    }

    public abstract class SpecialAbilityBase : ISpecialAbility
    {
        public Level Level { get; protected set; }
        public event EventHandler UnLocked;

        protected bool isUnlocked;

        public virtual void Unlock()
        {
            isUnlocked = true;
            UnLocked?.Invoke(this, EventArgs.Empty);
        }

        public abstract void Use();
        public abstract void OnLevelChanged(object sender, Level e);

    }


    public class AdvancingThePathway : SpecialAbilityBase
    {
        public override void Use()
        {
            if (!isUnlocked)
            {
                Debug.LogError("Ability is not unlocked.");
                return;
            }

            // Implement specific logic for using Advancing The Pathway
            Debug.Log("Using Advancing The Pathway!");
        }

        public override void OnLevelChanged(object sender, Level e)
        {
            // Implement logic for level changes specific to Advancing The Pathway
            Debug.Log($"Advancing The Pathway leveled up to level {e.Value}!");
        }

    }

    public class Tempest : SpecialAbilityBase
    {
        public override void Use()
        {
            if (!isUnlocked)
            {
                Debug.LogError("Ability is not unlocked.");
                return;
            }

            // Implement specific logic for using Tempest
            Debug.Log("Using Tempest!");
        }

        public override void OnLevelChanged(object sender, Level e)
        {
            // Implement logic for level changes specific to Tempest
            Debug.Log($"Tempest leveled up to level {e.Value}!");
        }
    }
    public class FlameWalker : SpecialAbilityBase
    {
        public override void Use()
        {
            if (!isUnlocked)
            {
                Debug.LogError("Ability is not unlocked.");
                return;
            }

            // Implement specific logic for using Tempest
            Debug.Log("Using Tempest!");
        }

        public override void OnLevelChanged(object sender, Level e)
        {
            // Implement logic for level changes specific to Tempest
            Debug.Log($"Tempest leveled up to level {e.Value}!");
        }
    }
}

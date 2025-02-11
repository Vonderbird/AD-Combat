using System;
using System.Linq;
using Sisus.Init;
using UnityEngine;
using UnityEngine.Serialization;

namespace ADC.API
{
    public interface ISpecialAbility
    {
        int UnlockLevel { get; }
        Sprite Icon { get; }
        string Name { get; }
        event EventHandler UnLocked;
        void Use();

        ISpecialAbility Initialize(IUnitBattleManager unitBattleManager);
        //void OnLevelChanged(object sender, LevelChangeEventArgs e);
    }

    [Serializable]
    public class SpecialAbilityCollection
    {
        [SerializeField] private Any<ISpecialAbility>[] specialAbilities;
        public ISpecialAbility[] SpecialAbilities => specialAbilities.Select(s => s.Value).ToArray();
    }

    public interface IReceivedDamageModifierAbility
    {
        int ModifyReceivedDamage(DamageArgs damage);
    }

    public interface IDealtDamageModifierAbility
    {
        int ModifyDealtDamage(DamageArgs damage);
    }
    public interface IHackerDamageModifierAbility
    {
        int HackThenDamage(DamageArgs damage);
    }

    [Flags]
    public enum DamageType
    {
        Ranged=1,
        Melee=2,
        Area=4,
    }

    public struct DamageArgs
    {
        public DamageArgs(IUnitBattleManager source, IUnitBattleManager target, 
            int value, DamageType damageType = DamageType.Melee, bool attackFromPostpone=false)
        {
            Source = source;
            Value = value;
            DamageType = damageType;
            Target = target;
            AttackFromPostpone = attackFromPostpone;
        }

        public IUnitBattleManager Source { get; set; }
        public IUnitBattleManager Target { get; set; }
        public DamageType DamageType { get; set; }
        public int Value { get; set; }
        public bool AttackFromPostpone { get; set; }
    }
}

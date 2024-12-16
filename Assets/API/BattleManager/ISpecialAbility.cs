using System;
using UnityEngine;

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

    public struct DamageArgs
    {
        public DamageArgs(IUnitBattleManager source, IUnitBattleManager target, 
            bool isRanged, bool isArea, int value, bool attackFromPostpone=false)
        {
            Source = source;
            IsRanged = isRanged;
            IsArea = isArea;
            Value = value;
            Target = target;
            AttackFromPostpone = attackFromPostpone;
        }

        public IUnitBattleManager Source { get; set; }
        public IUnitBattleManager Target { get; set; }
        public bool IsRanged { get; set; }
        public bool IsArea { get; set; }
        public int Value { get; set; }
        public bool AttackFromPostpone { get; set; }
    }
}

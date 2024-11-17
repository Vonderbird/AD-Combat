using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ADC.API
{
    public interface IEquipmentManager
    {
        UnitSpecs AddedSpecs { get; }
        HashSet<IAttackEquipment> AttackEquipments { get; }
        UnitEquipments Equipments { get; }
        void UpdateEquipments(UnitSpecs specsBaseSpecs, UnitEquipments baseEquipments);

        event EventHandler<EquipmentEventArgs> EquipmentRemoved;
        event EventHandler<EquipmentEventArgs> EquipmentAdded;
    }

    public interface IUnitSpecsManager
    {
        UnitSpecs BaseSpecs { get; }
        UnitSpecs CurrentSpecs { get; }
        void BindEquipmentSpecs(UnitSpecs addedSpecs);
        void UpdateBaseSpecs(UnitSpecs levelZeroSpecs);
        void Heal(int healingAmount);
        void ApplyBuff<T>(T buffAmount, float duration) where T : IUnitFeature<int, T>;
    }
    public interface IUnitBattleManager
    {
        //GameObject UnitObject { get; }
        //IEquipmentManager EquipmentManager { get; }
        //IUnitSpecsManager SpecsManager { get; }

        IUnitSpecsManager Specs { get;  }
        IEquipmentManager EquipmentManager { get; }
        List<ISpecialAbility> SpecialAbilities { get; }

        T GetComponent<T>() where T: Object;
    }
}

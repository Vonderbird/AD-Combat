using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ADC.API
{
    public interface IUnitUpdateInfo { }
    public interface IEquipmentManager
    {
        UnitSpecs AddedSpecs { get; }
        HashSet<IAttackEquipment> AttackEquipments { get; }
        UnitEquipments Equipments { get; }
        void Initialize(IUnitBattleManager unitBattleManager);
        void UpdateEquipments(UnitEquipments baseEquipments);

        event EventHandler<EquipmentEventArgs> EquipmentRemoved;
        event EventHandler<EquipmentEventArgs> EquipmentAdded; 
    }

    public interface IUnitSpecsManager
    {
        UnitSpecs BaseSpecs { get; }
        UnitSpecs CurrentSpecs { get; }
        public void Initialize(IThirdPartyInteractionManager thirdPartyManager);
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
        IUnitUpdateInfo UpdateInfo { get; }
        List<ISpecialAbility> SpecialAbilities { get; }

        public Transform Transform { get; }
        public GameObject GameObject { get; }
        T GetComponent<T>() where T: Object;
        T GetComponentInChildren<T>() where T : Object;
        T[] GetComponentsInChildren<T>() where T : Object;
    }
}

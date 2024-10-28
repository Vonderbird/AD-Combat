using System;
using System.Collections.Generic;
using UnityEngine;

namespace ADC
{

    public class DamageDictionary
    {
        private readonly Dictionary<Type, Dictionary<Type, float>> baseValues = new()
        {
            {typeof(Organic), new () {
                { typeof(BluntAttack), 1.0f },
                { typeof(Kinetic), 1.5f },
                { typeof(Plasma), 0.5f },
                { typeof(ExplosiveRounds), 0.75f },
                { typeof(Incendiary), 1.25f },
                { typeof(Biological), 1.0f } } },

            {typeof(LightTacticalAssault), new (){
                { typeof(BluntAttack), 1.0f },
                { typeof(Kinetic), 1.25f },
                { typeof(Plasma), 1.0f },
                { typeof(ExplosiveRounds), 1.0f },
                { typeof(Incendiary), 1.5f },
                { typeof(Biological), 1.0f }}},

            {typeof(Nano), new (){
                { typeof(BluntAttack), 1.0f },
                { typeof(Kinetic), 1.0f },
                { typeof(Plasma),1.0f },
                { typeof(ExplosiveRounds), 0.75f },
                { typeof(Incendiary), 1.00f },
                { typeof(Biological), 1.0f }}},

            {typeof(ScaledPlate), new (){
                { typeof(BluntAttack), 1.0f },
                { typeof(Kinetic), 0.5f },
                { typeof(Plasma), 1.5f },
                { typeof(ExplosiveRounds), 0.75f },
                { typeof(Incendiary), 0.75f },
                { typeof(Biological), 0.75f }}},

            {typeof(CarbonCompound), new (){
                { typeof(BluntAttack), 1.0f },
                { typeof(Kinetic), 1.0f },
                { typeof(Plasma), 0.75f },
                { typeof(ExplosiveRounds), 1.25f },
                { typeof(Incendiary), 0.5f },
                { typeof(Biological), 1.5f }}},

            {typeof(HeavyPlate), new (){
                { typeof(BluntAttack), 1.0f },
                { typeof(Kinetic), 0.75f },
                { typeof(Plasma), 1.25f },
                { typeof(ExplosiveRounds), 1.5f },
                { typeof(Incendiary), 0.75f },
                { typeof(Biological), 0.5f }}},
        };

        private readonly Dictionary<(Type, Type), float> valuesDict;

        public DamageDictionary()
        {
            //var baseValues = JsonUtility.FromJson<Dictionary<Type, Dictionary<Type, float>>>(jsonValues);
            valuesDict = new();
            foreach (var (shield, values) in baseValues)
            {
                foreach (var (weapon, damage) in values)
                {
                    valuesDict.Add((shield, weapon), damage);
                }
            }
        }
        private readonly Type shieldType = typeof(Shield);
        private readonly Type weaponType = typeof(Weapon);
        public float this[Type shield, Type weapon]
        {
            get
            {
                if (!shield.IsSubclassOf(shieldType))
                {
                    Debug.LogError($"[DamageDictionary] {shield.Name} is not subclass of {shieldType.Name}.");
                    return 0.0f;
                }

                if (!weapon.IsSubclassOf(weaponType))
                {
                    Debug.LogError($"[DamageDictionary] {weapon.Name} is not subclass of {weaponType.Name}.");
                    return 0.0f;
                }

                if (valuesDict.TryGetValue((shieldType, weaponType), out var value))
                {
                    return value;
                }
                else
                {
                    Debug.LogError($"[DamageDictionary] The damage value for {shield.Name} and {weapon.Name} is not defined.");
                    return 0.0f;
                }
            }
        }
    }

    public interface IWeaponArmorParams { }
    public class BluntAttackParams : IWeaponArmorParams { }
    public class KineticParams : IWeaponArmorParams { }
    public class PlasmaParams : IWeaponArmorParams { }
    public class ExplosiveRoundsParams : IWeaponArmorParams { }
    public class IncendiaryParams : IWeaponArmorParams { }
    public class BiologicalParams : IWeaponArmorParams { }


    public interface IArmorWeaponParams
    {
        void GetParam(BluntAttackParams args);
        void GetParam(KineticParams args);
        void GetParam(PlasmaParams args);
        void GetParam(ExplosiveRoundsParams args);
        void GetParam(IncendiaryParams args);
        void GetParam(BiologicalParams args);
    }

    public class OrganicParams : IArmorWeaponParams
    {
        public void GetParam(BluntAttackParams args)
        {
            throw new NotImplementedException();
        }

        public void GetParam(KineticParams args)
        {
            throw new NotImplementedException();
        }

        public void GetParam(PlasmaParams args)
        {
            throw new NotImplementedException();
        }

        public void GetParam(ExplosiveRoundsParams args)
        {
            throw new NotImplementedException();
        }

        public void GetParam(IncendiaryParams args)
        {
            throw new NotImplementedException();
        }

        public void GetParam(BiologicalParams args)
        {
            throw new NotImplementedException();
        }
    }

}
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
                { typeof(ElectroMagnetic), 1.0f },
                { typeof(Kinetic), 1.0f },
                { typeof(Plasma), 0.8f },
                { typeof(ExplosiveRounds), 1.0f },
                { typeof(Sharpened), 1.0f },
                { typeof(Biological), 1.0f } } },

            {typeof(LightTacticalAssault), new (){
                { typeof(ElectroMagnetic), 0.7f },
                { typeof(Kinetic), 0.9f },
                { typeof(Plasma), 1.1f },
                { typeof(ExplosiveRounds), 0.9f },
                { typeof(Sharpened), 1.6f },
                { typeof(Biological), 0.8f }}},

            {typeof(Nano), new (){
                { typeof(ElectroMagnetic), 1.1f },
                { typeof(Kinetic), 0.8f },
                { typeof(Plasma),1.3f },
                { typeof(ExplosiveRounds), 1.2f },
                { typeof(Sharpened), 1.00f },
                { typeof(Biological), 0.75f }}},

            {typeof(ScaledPlate), new (){
                { typeof(ElectroMagnetic), 0.9f },
                { typeof(Kinetic), 0.5f },
                { typeof(Plasma), 2.0f },
                { typeof(ExplosiveRounds), 0.5f },
                { typeof(Sharpened), 0.5f },
                { typeof(Biological), 0.5f }}},

            {typeof(CarbonCompound), new (){
                { typeof(ElectroMagnetic), 1.2f },
                { typeof(Kinetic), 1.0f },
                { typeof(Plasma), 0.5f },
                { typeof(ExplosiveRounds), 0.75f },
                { typeof(Sharpened), 0.5f },
                { typeof(Biological), 0.9f }}},

            {typeof(HeavyPlate), new (){
                { typeof(ElectroMagnetic), 1.0f },
                { typeof(Kinetic), 0.8f },
                { typeof(Plasma), 1.0f },
                { typeof(ExplosiveRounds), 1.75f },
                { typeof(Sharpened), 0.75f },
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
                    return 1.0f;
                }

                if (!weapon.IsSubclassOf(weaponType))
                {
                    Debug.LogError($"[DamageDictionary] {weapon.Name} is not subclass of {weaponType.Name}.");
                    return 1.0f;
                }

                if (shield == typeof(NoShield) || weapon == typeof(NoWeapon))
                    return 1.0f;

                if (valuesDict.TryGetValue((shield, weapon), out var value))
                {
                    return value;
                }
                else
                {
                    Debug.LogError($"[DamageDictionary] The damage value for {shield.Name} and {weapon.Name} is not defined.");
                    return 1.0f;
                }
            }
        }
    }
}
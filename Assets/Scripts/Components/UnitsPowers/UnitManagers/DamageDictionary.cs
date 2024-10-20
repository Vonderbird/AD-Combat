using System;
using System.Collections.Generic;
using UnityEngine;

namespace ADC
{
    public class DamageDictionary
    {
        private const string jsonValues = @"{""Organic"":
          {
            ""Blunt Attack"": ""100%"",
            ""Kinetic"": ""150%"",
            ""Arc"": ""50%"",
            ""Plasma"": ""50%"",
            ""Explosive Rounds"": ""75%"",
            ""Incendiary"": ""125%"",
            ""Biological"": ""100%""
          },
          ""Light Tactical Assault"": {
            ""Blunt Attack"": ""100%"",
            ""Kinetic"": ""125%"",
            ""Arc"": ""100%"",
            ""Plasma"": ""100%"",
            ""Explosive Rounds"": ""100%"",
            ""Incendiary"": ""150%"",
            ""Biological"": ""100%""
          },
          ""Nano"":{
            ""Blunt Attack"": ""150%"",
            ""Kinetic"": ""100%"",
            ""Arc"": ""100%"",
            ""Plasma"": ""50%"",
            ""Explosive Rounds"": ""75%"",
            ""Incendiary"": ""100%"",
            ""Biological"": ""100%""
          },
          ""Scaled Plate"": {
            ""Blunt Attack"": ""75%"",
            ""Kinetic"": ""50%"",
            ""Arc"": ""150%"",
            ""Plasma"": ""50%"",
            ""Explosive Rounds"": ""75%"",
            ""Incendiary"": ""75%"",
            ""Biological"": ""75%""
          },
          ""Carbon Compound"": {
            ""Blunt Attack"": ""100%"",
            ""Kinetic"": ""100%"",
            ""Arc"": ""75%"",
            ""Plasma"": ""50%"",
            ""Explosive Rounds"": ""125%"",
            ""Incendiary"": ""50%"",
            ""Biological"": ""150%""
          },
          ""Heavy Plate"": {
            ""Blunt Attack"": ""125%"",
            ""Kinetic"": ""75%"",
            ""Arc"": ""125%"",
            ""Plasma"": ""50%"",
            ""Explosive Rounds"": ""150%"",
            ""Incendiary"": ""75%"",
            ""Biological"": ""50%""
          }
        }";
        Dictionary<Type, Dictionary<Type, float>> basevalues = new Dictionary<Type, Dictionary<Type, float>>()
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
    foreach (var (armor, values) in basevalues)
    {
        foreach (var (weapon, damage) in values)
        {
            valuesDict.Add((armor, weapon), damage);
        }
    }
}
private readonly Type armorType = typeof(ArmorType);
private readonly Type weaponType = typeof(WeaponType);
public float this[Type armor, Type weapon]
{
    get
    {
        if (!armor.IsSubclassOf(armorType))
        {
            Debug.LogError($"[DamageDictionary] {armor.Name} is not subclass of {armorType.Name}.");
            return 0.0f;
        }

        if (!weapon.IsSubclassOf(weaponType))
        {
            Debug.LogError($"[DamageDictionary] {weapon.Name} is not subclass of {weaponType.Name}.");
            return 0.0f;
        }

        if (valuesDict.TryGetValue((armorType, weaponType), out var value))
        {
            return value;
        }
        else
        {
            Debug.LogError($"[DamageDictionary] The damage value for {armor.Name} and {weapon.Name} is not defined.");
            return 0.0f;
        }
    }
}
    }
}
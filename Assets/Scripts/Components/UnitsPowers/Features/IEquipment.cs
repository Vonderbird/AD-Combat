namespace ADC
{
    public interface IEquipment { }
    public interface IEquipment<in T> : IEquipment
    {
    }
    public interface IMagicalEquipment { int Mana { get; } }

    public interface IAttackEquipment
    {
        int UnitDamage { get; }
        int BuildingDamage { get; }

    }
    public interface IProtectorEquipment { int Armor { get; } }
    public interface IHitPointEquipment { int HitPoint { get; } }
}
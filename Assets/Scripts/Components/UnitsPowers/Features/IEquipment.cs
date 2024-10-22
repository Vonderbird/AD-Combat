namespace ADC
{
    public interface IEquipment { }
    public interface IMagicalEquipment { int Mana { get; } }
    public interface IAttackEquipment { int Damage { get; } }
    public interface IProtectorEquipment { int Armor { get; } }
    public interface IHitPointEquipment { int HitPoint { get; } }
}
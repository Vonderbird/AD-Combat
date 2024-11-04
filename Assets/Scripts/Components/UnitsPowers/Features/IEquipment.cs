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
    public interface IProtectorEquipment
    {
        int Armor { get; }
        void Defend();
        void Defend(Biological weapon);
        void Defend(Electromagnetic weapon);
        void Defend(ExplosiveRounds weapon);
        void Defend(Sharpened weapon);
        void Defend(Kinetic weapon);
        void Defend(Plasma weapon);
    }
    public interface IHitPointEquipment { int HitPoint { get; } }
}
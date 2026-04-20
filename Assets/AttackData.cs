using UnityEngine;

public struct AttackData
{
    public int Damage;
    public AttackType Type;

    public AttackData(int damage, AttackType type = AttackType.Physical)
    {
        Damage = damage;
        Type = type;
    }
}

public enum AttackType
{
    Physical,
    Fire,
    Ice
}
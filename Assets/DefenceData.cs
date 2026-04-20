using System.Collections.Generic;
using UnityEngine;

public struct DefenceData
{
    public int Armor;
    public Dictionary<AttackType, int> Resistnaces;

    public DefenceData(int armor, Dictionary<AttackType, int> resistnaces)
    {
        Armor = armor;
        Resistnaces = resistnaces;
    }
}

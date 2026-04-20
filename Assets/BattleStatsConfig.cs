using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleStats", menuName = "Scriptable Objects/BattleStats")]
public class BattleStatsConfig : ScriptableObject
{
    [field:SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int Attack { get; private set; }
    [field: SerializeField] public int Armor { get; private set; }
    [field: SerializeField] private ResistanceEntry[] resistances;

    public Dictionary<AttackType, int> Resistnaces;

    private void OnEnable()
    {
        Resistnaces = new Dictionary<AttackType, int>();
        foreach(ResistanceEntry resistanceEntry in resistances)
        {
            Resistnaces[resistanceEntry.Type] = resistanceEntry.Value;
        }
    }

}

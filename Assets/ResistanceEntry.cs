using System;
using UnityEngine;

[Serializable]
public struct ResistanceEntry
{
    [field: SerializeField] public AttackType Type { get; private set; }
    [field: SerializeField] public int Value { get; private set; }
}

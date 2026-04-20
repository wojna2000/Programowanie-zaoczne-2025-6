using UnityEngine;

[CreateAssetMenu(fileName = "GunStats", menuName = "Scriptable Objects/GunStats")]
public class GunStats : ScriptableObject
{
    [field: SerializeField] public float FireRate { get; private set; }
    public float ReloadTime;
    public float Damage;
    public string Name;
    public Sprite Icon;

}

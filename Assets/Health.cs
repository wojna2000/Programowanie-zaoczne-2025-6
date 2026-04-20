using System;
using UnityEngine;

[Serializable]
public class Health
{
    public event Action<int, int> OnHealthChanged;
    public int MaxHealth;
    [HideInInspector] public int CurrentHealth;

    public Health(int startingHealth)
    {
        MaxHealth = startingHealth;
        CurrentHealth = startingHealth;
    }

    public void TakeDamge(int damge)
    {
        CurrentHealth -= damge;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }
}

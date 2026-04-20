using System;
using UnityEngine;

[Serializable]
public class Health
{
    public int MaxHealth;
    [HideInInspector] public int currentHealth;

    public Health(int startingHealth)
    {
        MaxHealth = startingHealth;
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}


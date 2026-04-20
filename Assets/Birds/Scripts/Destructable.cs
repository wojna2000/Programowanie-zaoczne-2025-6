using System;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public event Action OnDeath;
    [SerializeField] private float health = 10;
    [SerializeField] private float velocityDamageMultiplier =1;

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health<=0)
        {
            OnDeath?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float damage = collision.relativeVelocity.magnitude * velocityDamageMultiplier;
        TakeDamage(damage);
    }
}

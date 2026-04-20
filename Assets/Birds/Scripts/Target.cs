using System;
using UnityEngine;

[RequireComponent(typeof(Destructable))]
public class Target : MonoBehaviour
{
    public event Action<Target> OnTargetDestoryed;
    private Destructable destructable;

    private void Start()
    {
        destructable = GetComponent<Destructable>();
        destructable.OnDeath += OnDeath;
    }

    private void OnDestroy()
    {
        destructable.OnDeath -= OnDeath;
    }

    private void OnDeath()
    {
        print("Target Destroyed");
        OnTargetDestoryed?.Invoke(this);
        Destroy(gameObject);
    }
}

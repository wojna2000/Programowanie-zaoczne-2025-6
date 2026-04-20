using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private Player player;
    private Health health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player.CallOrSubscribe(OnPlayerInitialized);
    }

    private void OnDestroy()
    {
        if(health != null)
        {
            health.OnHealthChanged -= OnHealthChanged;
        }
    }

    private void OnPlayerInitialized()
    {
        health = player.health;
        health.OnHealthChanged += OnHealthChanged;
        player.InitializeEvent -= OnPlayerInitialized;
    }

    private void OnHealthChanged(int currentHealth, int maxHealth)
    {
        bar.fillAmount = (float)currentHealth / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

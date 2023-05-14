using System;
using UnityEngine;

public class Stats : CoreComponent
{
    public event Action OnHealthZero;

    [SerializeField] private float maxHealth;
    [SerializeField] private int level;

    public float CurrentHealth {get; private set;}

    protected override void Awake()
    {
        base.Awake();

        maxHealth += (level * 20);
        CurrentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        CurrentHealth -= amount;

        if(CurrentHealth <= 0)
        {
            CurrentHealth = 0;

            OnHealthZero?.Invoke();
        }
    }

    public void IncreaseHealth(float amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, maxHealth);
    }
    
}

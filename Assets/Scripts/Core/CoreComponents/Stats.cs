using System;
using UnityEngine;
using UnityEngine.UI;

public class Stats : CoreComponent
{
    public event Action OnHealthZero;

    [SerializeField] private float maxHealth;

    [SerializeField] private Image healthBarFill;
    [SerializeField] private Canvas healthBar;

    private Vector3 healtBarRotation;

    public float CurrentHealth {get; private set;}

    protected override void Awake()
    {
        base.Awake();
        
        CurrentHealth = maxHealth;

        healtBarRotation = Vector3.zero;

        if (healthBar != null)
            healthBar.gameObject.SetActive(false);
    }

    public void LogicUpdate()
    {
        if (healthBarFill != null)
            healthBarFill.fillAmount = CurrentHealth/maxHealth;

        if (healthBar != null)
        {
            if (CurrentHealth < maxHealth && CurrentHealth > 0 && !healthBar.gameObject.activeSelf)
                healthBar.gameObject.SetActive(true);
        }
        
    }

    public void LateUpdate()
    {
        if (healthBar != null)
            healthBar.transform.rotation = Quaternion.identity;
    }

    public void DecreaseHealth(float amount)
    {
        CurrentHealth -= amount;

        if(CurrentHealth <= 0)
        {
            CurrentHealth = 0;

            if(healthBar != null)
                healthBar.gameObject.SetActive(false);

            OnHealthZero?.Invoke();
        }
    }

    public void IncreaseHealth(float amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, maxHealth);
    }
}

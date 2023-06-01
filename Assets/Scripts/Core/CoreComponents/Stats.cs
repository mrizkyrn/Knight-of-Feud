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

    private float healthPercent;
    private bool isPlayer;

    protected override void Awake()
    {
        base.Awake();
        
        isPlayer = transform.parent.parent.CompareTag("Player");

        CurrentHealth = isPlayer? PlayerStats.Instance.Health.MaxValue : maxHealth;

        healtBarRotation = Vector3.zero;

        if (healthBar != null)
            healthBar.gameObject.SetActive(false);
    }

    public void LogicUpdate()
    {
        healthPercent = isPlayer? (PlayerStats.Instance.Health.CurrentValue/PlayerStats.Instance.Health.MaxValue) : CurrentHealth/maxHealth;
        if (healthBarFill != null)
            healthBarFill.fillAmount = healthPercent;

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
        if (!isPlayer)
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
        else
        {
            PlayerStats.Instance.Health.Decrease(amount);
            if(PlayerStats.Instance.Health.CurrentValue <= 0)
            {
                OnHealthZero?.Invoke();
            }
        }
        
    }

    public void IncreaseHealth(float amount)
    {
        if (!isPlayer)
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, maxHealth);
    }

    public bool IsHealthZero(float amount)
    {
        if (isPlayer)
            return PlayerStats.Instance.Health.CurrentValue - amount > 0;
        else
            return CurrentHealth - amount > 0;
    }
}

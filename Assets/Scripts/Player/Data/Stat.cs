using System;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public event Action OnCurrentValueZero;

    [field: SerializeField] public float BaseValue { get; private set; }

    public float CurrentValue
    {
        get => currentValue;
        private set
        {
            currentValue = Mathf.Clamp(value, 0f, BaseValue);

            if (currentValue <= 0f)
            {
                OnCurrentValueZero?.Invoke();
            }
        }
    }
    
    private float currentValue;

    public void Init() => CurrentValue = BaseValue;

    public void Increase(float amount) => CurrentValue += amount;

    public void Decrease(float amount) => CurrentValue -= amount;
}

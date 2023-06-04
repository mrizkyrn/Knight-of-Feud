using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public event Action OnCurrentValueZero;

    [field: SerializeField] public float BaseValue { get; private set; }

    public float MaxValue { get; private set; }

    private float modifiedValue;
    private List<float> modifiers = new List<float>();

    public float ModifiedValue => modifiedValue;

    public float CurrentValue
    {
        get => currentValue;
        private set
        {
            currentValue = Mathf.Clamp(value, 0f, MaxValue);

            if (currentValue <= 0f)
            {
                OnCurrentValueZero?.Invoke();
            }
        }
    }

    private float currentValue;

    public void Init(float maxValue)
    {
        MaxValue = maxValue;
        
        CurrentValue = BaseValue;
        modifiedValue = BaseValue;
    }

    public void Increase(float amount) => CurrentValue += amount;

    public void Decrease(float amount) => CurrentValue -= amount;

    public void IncreaseMaxValue(float amount) => MaxValue += amount;

    public void DecreaseMaxValue(float amount) => MaxValue -= amount;

    public void SetZero() => CurrentValue = 0f;

    public void AddModifier(float modifier)
    {
        modifiers.Add(modifier);
        UpdateModifiedValue();
    }

    public void RemoveModifier(float modifier)
    {
        modifiers.Remove(modifier);
        UpdateModifiedValue();
    }

    private void UpdateModifiedValue()
    {
        float totalModifier = 0f;

        foreach (float modifier in modifiers)
        {
            totalModifier += modifier;
        }

        modifiedValue = BaseValue + totalModifier;
    }
}

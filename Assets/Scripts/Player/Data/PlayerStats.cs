using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public int Level { get; private set; }

    [field: SerializeField] public Stat Health { get; private set; }
    [field: SerializeField] public Stat XP { get; private set; }
    [field: SerializeField] public Stat Gold { get; private set; }
    [field: SerializeField] public Stat Damage { get; private set; }
    [field: SerializeField] public Stat ShieldDurability { get; private set; }

    public event Action OnLevelUp;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Level = 1;

        Health.Init(100);
        XP.Init(100);
        Gold.Init(999999);
        Damage.Init(999999);
        ShieldDurability.Init(50);
    }

    public void LevelUp()
    {
        Debug.Log("LEVEL UP");
        XP.IncreaseMaxValue(XP.MaxValue * 0.5f);
        XP.SetZero();

        Level++;

        OnLevelUp?.Invoke();
    }

    public void IncreaseXP(float amount)
    {
        float remainingXP = amount;

        while (remainingXP > 0)
        {
            float xpToIncrease = Mathf.Min(remainingXP, XP.MaxValue - XP.CurrentValue);
            XP.Increase(xpToIncrease);
            remainingXP -= xpToIncrease;
            
            if (XP.CurrentValue >= XP.MaxValue)
            {
                LevelUp();
            }
        }
    }
}

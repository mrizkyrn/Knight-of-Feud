using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [field: SerializeField] public Stat Health { get; private set; }
    [field: SerializeField] public Stat XP { get; private set; }
    [field: SerializeField] public Stat Gold { get; private set; }
    [field: SerializeField] public Stat Damage { get; private set; }
    [field: SerializeField] public Stat ShieldDurability { get; private set; }

    public int level;

    public float maxHealth;
    public float maxXP;
    public float maxShiledDurablity;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    
        Health.Init();
        XP.Init();
        Gold.Init();
        Damage.Init();
        ShieldDurability.Init();

        maxXP = 100f;
        maxShiledDurablity = ShieldDurability.BaseValue;
    }

    public void LevelUp()
    {
        
    }


}

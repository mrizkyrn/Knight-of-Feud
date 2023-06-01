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

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    
        Health.Init(100);
        XP.Init(100);
        Gold.Init(999999);
        Damage.Init(999999);
        ShieldDurability.Init(50);
    }

    public void LevelUp()
    {
        
    }


}

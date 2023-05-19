using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    [field: SerializeField] public Stat Health { get; private set; }
    [field: SerializeField] public Stat XP { get; private set; }
    [field: SerializeField] public Stat Gold { get; private set; }
    [field: SerializeField] public Stat Damage { get; private set; }

    public int level;

    public float maxHealth;
    public float maxXP;

    private void Awake()
    {
        Health.Init();
        XP.Init();
        Gold.Init();
        Damage.Init();

        maxXP = 100f;
    }

    public void LevelUp()
    {
        
    }


}
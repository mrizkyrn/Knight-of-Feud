using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable
{
    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged!");
        core.Stats.DecreaseHealth(amount);
    }
}

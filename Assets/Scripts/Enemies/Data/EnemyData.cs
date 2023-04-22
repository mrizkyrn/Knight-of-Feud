using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newEnemyData", menuName ="Data/Enemy Data/Base Data")]

public class EnemyData : ScriptableObject
{
    [Header("Idle State")]
    public float minIdleTime;
    public float maxIdleTime;

    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Others")]
    public float maxHealth = 30f;

    public float damageHopSpeed = 3f;

    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckRadius = 0.3f;

    public float minAgroDistance = 3f;
    public float maxAgroDistance = 4f;

    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;

    public float closeRangeActionDistance = 1f;

    public GameObject hitParticle;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}

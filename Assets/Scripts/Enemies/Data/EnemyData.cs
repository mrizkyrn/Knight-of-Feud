using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newEnemyData", menuName ="Data/Enemy Data/Base Data")]

public class EnemyData : ScriptableObject
{
    [Header("Idle State")]
    public float minIdleTime = 1f;
    public float maxIdleTime = 2f;

    [Header("Move State")]
    public float movementVelocity = 3f;

    [Header("Player Detected State")]
    public float longRangeActionTime = 1.5f;

    [Header("Charge State")]
    public float chargeSpeed = 6f;
    public float chargeTime = 2f;

    [Header("Look For Player State")]
    public int amountOfTurns = 2;
    public float timeBetweenTurns = 0.75f;

    [Header("Melee Attack State")]
    public float meleeAttackRadius = 0.5f;
    public float meleeAttackDamage = 10f;

    public Vector2 knockbackAngle = Vector2.one;
    public float knockbackStrength = 10f;
    public float knockbackStrengthShield = 5f;


    [Header("Others")]
    public float maxHealth = 30f;

    public float damageHopSpeed = 3f;

    // public float wallCheckDistance = 0.2f;
    // public float ledgeCheckDistance = 0.4f;
    // public float groundCheckRadius = 0.3f;

    public float minAgroDistance = 3f;
    public float maxAgroDistance = 4f;

    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;

    public float closeRangeActionDistance = 1f;

    public GameObject hitParticle;
    
    public LayerMask playerLayerMask;
    public LayerMask platformLayerMask;
}

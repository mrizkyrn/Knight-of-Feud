using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
	private Movement movement;

    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyData enemyData;
    public Core Core { get; private set; }

    public Animator Anim { get; private set; }

    public int lastDamageDirection { get; private set; }

    [SerializeField] private Transform playerCheck;

    private float currentHealth;
	private float currentStunResistance;
	private float lastDamageTime;

    private Vector2 velocityWorkspace;

	protected bool isStunned;
	protected bool isDead;

	public virtual void Awake() 
    {
		Core = GetComponentInChildren<Core>();

		currentHealth = enemyData.maxHealth;
		currentStunResistance = enemyData.stunResistance;

		Anim = GetComponent<Animator>();

		StateMachine = new EnemyStateMachine();
	}

    public virtual void Update() 
    {
		StateMachine.CurrentState.LogicUpdate();

		if (Time.time >= lastDamageTime + enemyData.stunRecoveryTime) 
        {
			ResetStunResistance();
		}
	}

	public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    } 

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, enemyData.minAgroDistance, enemyData.playerLayerMask);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, enemyData.maxAgroDistance, enemyData.playerLayerMask);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, enemyData.closeRangeActionDistance, enemyData.playerLayerMask);
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(Core.Movement.Rb.velocity.x, velocity);
        Core.Movement.Rb.velocity = velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = enemyData.stunResistance;
    }

    // public virtual void OnDrawGizmos()
    // {
    //     if(Core != null)
    //     {
    //         Gizmos.DrawLine(Core.CollisionSenses.Col.bounds.center, Core.CollisionSenses.Col.bounds.center + (Vector3)(Vector2.right * Core.Movement.FacingDirection * enemyData.wallCheckDistance));
    //         Gizmos.DrawLine(Core.CollisionSenses.Col.bounds.center, Core.CollisionSenses.Col.bounds.center + (Vector3)(Vector2.down * enemyData.ledgeCheckDistance));

    //         Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * enemyData.closeRangeActionDistance), 0.2f);
    //         Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * enemyData.minAgroDistance), 0.2f);
    //         Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * enemyData.maxAgroDistance), 0.2f);
    //     }       
    // }
}

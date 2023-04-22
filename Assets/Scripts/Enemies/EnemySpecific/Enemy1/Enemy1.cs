using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState IdleState { get; private set; }
    public E1_MoveState MoveState { get; private set; }

    public override void Awake()
    {
        base.Awake();

        IdleState = new E1_IdleState(this, "idle", this);
        MoveState = new E1_MoveState(this, "move", this);

        StateMachine.Initialize(IdleState);
    }

    // public override void OnDrawGizmos()
    // {
    //     base.OnDrawGizmos();

    //     Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    // }

    // public override void Damage(AttackDetails attackDetails)
    // {
    //     base.Damage(attackDetails);

    //     if (isDead)
    //     {
    //         stateMachine.ChangeState(deadState);
    //     }
    //     else if (isStunned && stateMachine.currentState != stunState)
    //     {
    //         stateMachine.ChangeState(stunState);
    //     }        
    // }
}

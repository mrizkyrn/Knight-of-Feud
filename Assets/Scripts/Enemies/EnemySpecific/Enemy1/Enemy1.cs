using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState IdleState { get; private set; }
    public E1_MoveState MoveState { get; private set; }
    public E1_PlayerDetectedState PlayerDetectedState { get; private set; }
    public E1_ChargeState ChargeState { get; private set; }
    public E1_LookForPlayerState LookForPlayerState { get; private set; }

    public override void Awake()
    {
        base.Awake();

        IdleState = new E1_IdleState(this, "idle", this);
        MoveState = new E1_MoveState(this, "move", this);
        PlayerDetectedState = new E1_PlayerDetectedState(this, "playerDetected", this);
        ChargeState = new E1_ChargeState(this, "charge", this);
        LookForPlayerState = new E1_LookForPlayerState(this, "lookForPlayer", this);

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

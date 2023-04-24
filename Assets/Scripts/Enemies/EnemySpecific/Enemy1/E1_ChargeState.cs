using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChargeState : EnemyChargeState
{
    Enemy1 enemy;

    public E1_ChargeState(Entity entity, string animBoolName, Enemy1 enemy) : base(entity, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // if (performCloseRangeAction)
        // {
        //     entity.StateMachine.ChangeState(enemy.meleeAttackState);
        // }
        // else 
        if (!isLedged || isWalled || !isPlayerInMinAgroRange)
        {
            entity.StateMachine.ChangeState(enemy.LookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                entity.StateMachine.ChangeState(enemy.PlayerDetectedState);
            }
            else
            {
                entity.StateMachine.ChangeState(enemy.LookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}

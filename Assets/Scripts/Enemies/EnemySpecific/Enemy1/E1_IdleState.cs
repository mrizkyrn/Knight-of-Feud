using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_IdleState : EnemyIdleState
{
    private Enemy1 enemy;

    public E1_IdleState(Entity entity, string animBoolName, Enemy1 enemy) : base(entity, animBoolName)
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

        
        if (isPlayerInMinAgroRange && isLedged)
        {
                entity.StateMachine.ChangeState(enemy.PlayerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            entity.StateMachine.ChangeState(enemy.MoveState);
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

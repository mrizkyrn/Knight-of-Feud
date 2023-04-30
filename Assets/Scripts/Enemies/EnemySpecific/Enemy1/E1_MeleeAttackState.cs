using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MeleeAttackState : EnemyMeleeAttackState
{
    Enemy1 enemy;

    public E1_MeleeAttackState(Entity entity, string animBoolName, Transform attackPosition, Enemy1 enemy) : base(entity, animBoolName, attackPosition)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MoveState : EnemyMoveState
{
    Enemy1 enemy;

    public E1_MoveState(Entity entity, string animBoolName, Enemy1 enemy) : base(entity, animBoolName)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Debug.Log(isLedged);

        if (isWalled || !isLedged)
        {
            enemy.IdleState.SetFlipAfterIdle(true);
            entity.StateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

}

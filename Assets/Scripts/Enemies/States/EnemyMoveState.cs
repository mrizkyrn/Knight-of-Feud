using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    protected bool isWalled;
    protected bool isLedged;
    protected bool isPlayerInMinAgroRange;

    public EnemyMoveState(Entity entity, string animBoolName) : base(entity, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        entity.Core.Movement.SetVelocityX(entity.enemyData.movementVelocity * entity.Core.Movement.FacingDirection);
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
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isWalled = entity.Core.CollisionSenses.CheckIfWalled();
        isLedged = entity.Core.CollisionSenses.CheckIfLedged();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }
}

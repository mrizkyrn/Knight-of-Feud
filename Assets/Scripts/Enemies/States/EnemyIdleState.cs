using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAgroRange;
    protected bool isLedged;

    protected float idleTime;

    public EnemyIdleState(Entity entity, string animBoolName) : base(entity, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        entity.Core.Movement?.SetVelocityX(0f);
        isIdleTimeOver = false;        
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterIdle)
        {
            entity.Core.Movement?.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (entity.Core.Movement.CurrentVelocity.x != 0f)
		{
			entity.Core.Movement?.SetVelocityZero();
		}

        if(Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();        
    }

    public override void DoChecks()
    {
        base.DoChecks();
		isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isLedged = entity.Core.CollisionSenses.CheckIfLedged();
    }
    
    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(entity.enemyData.minIdleTime, entity.enemyData.maxIdleTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAgroRange;

    protected float idleTime;

    public EnemyIdleState(Entity entity, string animBoolName) : base(entity, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        entity.Core.Movement.SetVelocityX(0f);
        isIdleTimeOver = false;        
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterIdle)
        {
            entity.Core.Movement.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

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

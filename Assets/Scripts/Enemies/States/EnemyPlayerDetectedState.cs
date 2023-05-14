using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetectedState : EnemyState
{
	protected bool isPlayerInMinAgroRange;
	protected bool isPlayerInMaxAgroRange;
	protected bool performLongRangeAction;
	protected bool performCloseRangeAction;
	protected bool isLedged;

    public EnemyPlayerDetectedState(Entity entity, string animBoolName) : base(entity, animBoolName)
    {
    }

	public override void Enter() 
	{
		base.Enter();

		performLongRangeAction = false;
		entity.Core.Movement?.SetVelocityX(0f);
	}

	public override void Exit() 
	{
		base.Exit();
	}

	public override void LogicUpdate() 
	{
		base.LogicUpdate();

		if (entity.Core.Movement.CurrentVelocity.x != 0f)
		{
			entity.Core.Movement?.SetVelocityZero();
		}

		if (Time.time >= startTime + entity.enemyData.longRangeActionTime) 
		{
			performLongRangeAction = true;
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
		isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
		isLedged = entity.Core.CollisionSenses.CheckIfLedged();
		performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
	}
}

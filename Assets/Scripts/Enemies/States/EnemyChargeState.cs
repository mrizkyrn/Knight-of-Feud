using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeState : EnemyState
{
	protected bool isLedged;
	protected bool isWalled;
    protected bool isPlayerInMinAgroRange;
	protected bool performCloseRangeAction;

    public EnemyChargeState(Entity entity, string animBoolName) : base(entity, animBoolName)
    {
    }

    public override void Enter() 
    {
		base.Enter();

		entity.Core.Movement?.SetVelocityX(entity.enemyData.chargeSpeed * entity.Core.Movement.FacingDirection);
	}

	public override void Exit() 
    {
		base.Exit();
	}

	public override void LogicUpdate() 
    {
		base.LogicUpdate();

		if (entity.Core.Movement.CurrentVelocity.x != entity.enemyData.chargeSpeed * entity.Core.Movement.FacingDirection)
		{
			entity.Core.Movement?.SetVelocityX(entity.enemyData.chargeSpeed * entity.Core.Movement.FacingDirection);
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
        isWalled = entity.Core.CollisionSenses.CheckIfWalled();
        isLedged = entity.Core.CollisionSenses.CheckIfLedged();

		performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    protected Transform attackPosition;

    protected bool isAnimationFinished;
	protected bool isPlayerInMinAgroRange;
	protected bool isPlayerShielding;

    public EnemyAttackState(Entity entity, string animBoolName, Transform attackPosition) : base(entity, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void Enter() {
		base.Enter();

        entity.Atsm.attackState = this;
		isAnimationFinished = false;
		entity.Core.Movement?.SetVelocityX(0f);
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();

		if (entity.Core.Movement.CurrentVelocity.x != 0f)
		{
			entity.Core.Movement?.SetVelocityZero();
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}

    public override void DoChecks() {
		base.DoChecks();

		isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
		isPlayerShielding = entity.player.IsShielding && entity.Core.Movement.FacingDirection != entity.player.Core.Movement.FacingDirection;
	}

    public virtual void TriggerAttack() {

	}

	public virtual void FinishAttack() {
		isAnimationFinished = true;
	}
}

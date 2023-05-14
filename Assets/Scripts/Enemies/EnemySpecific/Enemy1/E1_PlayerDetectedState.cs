using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : EnemyPlayerDetectedState
{
    Enemy1 enemy;

    public E1_PlayerDetectedState(Entity entity, string animBoolName, Enemy1 enemy) : base(entity, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter() {
		base.Enter();
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();

		if (performCloseRangeAction) 
		{
			entity.StateMachine.ChangeState(enemy.MeleeAttackState);
		} 
        else if (performLongRangeAction) 
		{
			entity.StateMachine.ChangeState(enemy.ChargeState);
		} 
		else if (!isPlayerInMaxAgroRange) 
        {
			entity.StateMachine.ChangeState(enemy.LookForPlayerState);
		} 
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}

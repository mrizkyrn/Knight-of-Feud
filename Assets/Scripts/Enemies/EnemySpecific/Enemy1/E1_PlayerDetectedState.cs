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

		// if (performCloseRangeAction) {
		// 	stateMachine.ChangeState(enemy.meleeAttackState);
		// } 
        if (performLongRangeAction) {
			entity.StateMachine.ChangeState(enemy.ChargeState);
		} else if (!isPlayerInMaxAgroRange) 
        {
			entity.StateMachine.ChangeState(enemy.LookForPlayerState);
		} 
        else if (!isLedged) 
        {
			entity.Core.Movement?.Flip();
			entity.StateMachine.ChangeState(enemy.MoveState);
		}

	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}

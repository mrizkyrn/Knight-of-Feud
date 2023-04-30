using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyAttackState
{
    public EnemyMeleeAttackState(Entity entity, string animBoolName, Transform attackPosition) : base(entity, animBoolName, attackPosition)
    {
    }

    public override void Enter() {
		base.Enter();
	}

	public override void Exit() {
		base.Exit();
	}

	public override void FinishAttack() {
		base.FinishAttack();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}

    public override void DoChecks() {
		base.DoChecks();
	}

    public override void TriggerAttack() {
		base.TriggerAttack();

		Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, entity.enemyData.meleeAttackRadius, entity.enemyData.playerLayerMask);

        Debug.Log(entity.enemyData.meleeAttackDamage);
		foreach (Collider2D collider in detectedObjects) 
        {
			IDamageable damageable = collider.GetComponent<IDamageable>();

			if (damageable != null) 
            {
				damageable.Damage(entity.enemyData.meleeAttackDamage);
			}

		// 	IKnockbackable knockbackable = collider.GetComponent<IKnockbackable>();

		// 	if (knockbackable != null) {
		// 		knockbackable.Knockback(entity.enemyData.knockbackAngle, entity.enemyData.knockbackStrength, Movement.FacingDirection);
		// 	}
		}
	}
}

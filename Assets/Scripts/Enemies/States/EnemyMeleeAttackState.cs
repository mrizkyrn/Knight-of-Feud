using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyAttackState
{
    public EnemyMeleeAttackState(Entity entity, string animBoolName, Transform attackPosition) : base(entity, animBoolName, attackPosition)
    {
    }

    public override void Enter() 
	{
		base.Enter();
	}

	public override void Exit() 
	{
		base.Exit();
	}

	public override void FinishAttack() 
	{
		base.FinishAttack();
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
	}

    public override void TriggerAttack() 
	{
		base.TriggerAttack();

		Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, entity.enemyData.meleeAttackRadius, entity.enemyData.playerLayerMask);

		foreach (Collider2D collider in detectedObjects) 
        {
			IDamageable damageable = collider.GetComponent<IDamageable>();

			if (damageable != null) 
            {
				float newDamage = Mathf.Max(entity.enemyData.meleeAttackDamage - PlayerStats.Instance.Defense.CurrentValue, 0f);

				if (isPlayerShielding)
				{
					PlayerStats.Instance.ShieldDurability.Decrease(newDamage);
					entity.player.PlaySoundEffect("Shield", 0.3f);
				}
				else
				{
					damageable.Damage(newDamage);
					entity.player.PlaySoundEffect("TakeHit", 0.3f);
				}
			}

			IKnockbackable knockbackable = collider.GetComponent<IKnockbackable>();

			if (knockbackable != null)
			{
				float knockbackStrength = isPlayerShielding ? entity.enemyData.knockbackStrengthShield : entity.enemyData.knockbackStrength;

				knockbackable.Knockback(entity.enemyData.knockbackAngle, knockbackStrength, entity.Core.Movement.FacingDirection);
			}
		}
	}
}

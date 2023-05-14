using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    [SerializeField] private float maxKnockbackTime = 0.2f;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject damageParticles;

    private bool isKnockbackActive;
	private float knockbackStartTime;


	public void LogicUpdate() 
    {
        if (isKnockbackActive)
        {
		    CheckKnockback();
        }
	}

    public void Damage(float amount)
    {
        if(animator != null)
        {
            if (core.Stats.CurrentHealth - amount > 0)
            {
                animator.SetTrigger("TakeHit");
            }
            else
            {
                animator.SetTrigger("Death");
            }
        }

        core.Stats.DecreaseHealth(amount);
        core.ParticleManager?.StartParticlesWithRandomRotation(damageParticles);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        core.Movement?.SetVelocity(strength, angle, direction);
        core.Movement.CanSetVelocity = false;
        isKnockbackActive = true;
		knockbackStartTime = Time.time;
    }

    private void CheckKnockback() 
    {
		if ((core.Movement?.CurrentVelocity.y <= 0.01f && core.CollisionSenses.CheckIfGrounded())
			|| Time.time >= knockbackStartTime + maxKnockbackTime)
        {
			isKnockbackActive = false;
			core.Movement.CanSetVelocity = true;
		}
	}
}

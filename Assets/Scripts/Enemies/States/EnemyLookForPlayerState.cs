using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookForPlayerState : EnemyState
{
    protected bool turnImmediately;
	protected bool isPlayerInMinAgroRange;
	protected bool isAllTurnsDone;
	protected bool isAllTurnsTimeDone;

	protected float lastTurnTime;

	protected int amountOfTurnsDone;

    public EnemyLookForPlayerState(Entity entity, string animBoolName) : base(entity, animBoolName)
    {
    }

    public override void Enter() 
    {
		base.Enter();

		isAllTurnsDone = false;
		isAllTurnsTimeDone = false;

		lastTurnTime = startTime;
		amountOfTurnsDone = 0;

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

		if (turnImmediately) 
        {
			entity.Core.Movement?.Flip();
			lastTurnTime = Time.time;
			amountOfTurnsDone++;
			turnImmediately = false;
		} 
        else if (Time.time >= lastTurnTime + entity.enemyData.timeBetweenTurns && !isAllTurnsDone) 
        {
			entity.Core.Movement?.Flip();
			lastTurnTime = Time.time;
			amountOfTurnsDone++;
		}

		if (amountOfTurnsDone >= entity.enemyData.amountOfTurns) 
        {
			isAllTurnsDone = true;
		}

		if (Time.time >= lastTurnTime + entity.enemyData.timeBetweenTurns && isAllTurnsDone) 
        {
			isAllTurnsTimeDone = true;
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
	}

    public void SetTurnImmediately(bool flip) 
    {
		turnImmediately = flip;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    private Vector2 perpendicularSpeed;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.CheckIfShouldFlip(xInput);
        perpendicularSpeed = Vector2.Perpendicular(core.CollisionSenses.hitSlope.normal).normalized;

        if (isOnSlope)
        {
            core.Movement.SetVelocityX(playerData.movementVelocity * -xInput * perpendicularSpeed.x);
        }
        else
        {
            core.Movement.SetVelocityX(playerData.movementVelocity * xInput);
        }

        if (xInput == 0 && !isExitingState)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

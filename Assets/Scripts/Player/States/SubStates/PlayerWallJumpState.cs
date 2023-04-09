using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    private bool isWalled;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // player.InputHandler.UseJumpInput();
        player.JumpState.ResetJumpsLeft();
        core.Movement.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        core.Movement.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.DecreaseWallJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }

        if (isWalled)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isWalled = core.CollisionSenses.CheckIfWalled();
    }

    public void DetermineWallJumpDirection(bool isWalled)
    {
        if (isWalled)
        {
            wallJumpDirection = -core.Movement.FacingDirection;
        }
        else
        {
            wallJumpDirection = core.Movement.FacingDirection;
        }
    }

}

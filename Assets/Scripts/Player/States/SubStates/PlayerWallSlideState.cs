using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public bool fallFromWall { get; private set; }

    private bool isGrounded;
    private bool isWalled;
    private int xInput;

    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.DecreaseJumpsLeft();
        player.Rb.gravityScale = 0;

        player.InputHandler.OnAttackDisable();
    }

    public override void Exit()
    {
        base.Exit();

        player.Rb.gravityScale = 5;

        player.InputHandler.OnAttackEnable();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;

        if (xInput == player.FacingDirection)
        {
            player.SetVelocityY(-playerData.grabWallVelocity);
        }
        else
        {
            player.SetVelocityY(-playerData.wallSlideVelocity);
        }

        if (player.InputHandler.JumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(isWalled);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (!isWalled || xInput == -player.FacingDirection)
        {
            fallFromWall = true;
            stateMachine.ChangeState(player.FallState);
        }
        else if (isGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isWalled = player.CheckIfWalled();
    }

    public void ResetFallFromWall() => fallFromWall = false;
}

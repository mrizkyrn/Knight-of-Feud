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
        core.Movement.SetGravity(0);

        player.InputHandler.OnAttackDisable();
    }

    public override void Exit()
    {
        base.Exit();

        core.Movement.SetGravity(playerData.gravityScale);

        player.InputHandler.OnAttackEnable();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;

        if (xInput == core.Movement.FacingDirection)
        {
            core.Movement.SetVelocityY(-playerData.grabWallVelocity);
        }
        else
        {
            core.Movement.SetVelocityY(-playerData.wallSlideVelocity);
        }

        if (player.InputHandler.JumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(isWalled);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (!isWalled || xInput == -core.Movement.FacingDirection)
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

        isGrounded = core.CollisionSenses.CheckIfGrounded();
        isWalled = core.CollisionSenses.CheckIfWalled();
    }

    public void ResetFallFromWall() => fallFromWall = false;
}

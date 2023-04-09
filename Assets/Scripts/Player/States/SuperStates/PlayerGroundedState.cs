using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Vector2 input;
    protected int xInput;
    protected int yInput;

    private bool jumpInput;
    private bool slideInput;
    private bool attackInput;
    private bool isGrounded;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetJumpsLeft();
        player.SlideState.ResetCanSlide();
        player.WallSlideState.ResetFallFromWall();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;

        input = player.InputHandler.MovementInput;
        jumpInput = player.InputHandler.JumpInput;
        slideInput = player.InputHandler.SlideInput;
        attackInput = player.InputHandler.AttackInput;

        if (jumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (attackInput && player.AttackState.CheckIfCanAttack())
        {
            stateMachine.ChangeState(player.AttackState);
        }
        else if (slideInput && player.SlideState.CheckIfCanSlide())
        {
            player.InputHandler.UseSlideInput();
            stateMachine.ChangeState(player.SlideState);
        }
        else if (!isGrounded && core.Movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.FallState);
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
    }
}

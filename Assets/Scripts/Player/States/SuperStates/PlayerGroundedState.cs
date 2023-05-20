using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Vector2 input;
    protected int xInput;
    protected int yInput;
    protected bool isSloped;
    protected bool isOnSlope;
    protected float slopeAngle;


    private bool jumpInput;
    private bool slideInput;
    private bool attackInput;
    private bool shieldInput;
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

        core.Movement.Rb.sharedMaterial = playerData.noFrictionMaterial;
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
        shieldInput = player.InputHandler.ShieldInput;

        SlopeUpdate();

        if (jumpInput && player.JumpState.CheckIfCanJump())
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
            stateMachine.ChangeState(player.SlideState);
        }
        else if (shieldInput && player.playerStats.ShieldDurability.CurrentValue > 0)
        {
            stateMachine.ChangeState(player.ShieldState);
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
        isSloped = core.CollisionSenses.CheckIfSloped();
    }

    private void SlopeUpdate()
    {
        if (isSloped)
        {
            Vector2 p = Vector2.Perpendicular(core.CollisionSenses.hitSlope.normal).normalized;
            slopeAngle = Vector2.Angle(core.CollisionSenses.hitSlope.normal, Vector2.up);
            isOnSlope = slopeAngle != 0;
        }

        if (isOnSlope && xInput == 0)
        {
            if (core.Movement.Rb.sharedMaterial != playerData.frictionMaterial)
            {
                core.Movement.Rb.sharedMaterial = playerData.frictionMaterial;
            }
        }
        else
        {
            if (core.Movement.Rb.sharedMaterial != playerData.noFrictionMaterial)
            {
                core.Movement.Rb.sharedMaterial = playerData.noFrictionMaterial;
            }
        }
    }
}

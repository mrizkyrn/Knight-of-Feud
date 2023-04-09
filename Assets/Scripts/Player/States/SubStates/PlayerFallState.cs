using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerState
{
    private bool isGrounded;
    private bool isWalled;
    private bool isFallFromWall;
    private bool jumpInput;
    private bool attackInput;

    private int xInput;

    public PlayerFallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
         player.Anim.SetBool("isGrounded", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        attackInput = player.InputHandler.AttackInput;

        if (jumpInput && player.JumpState.CanJump() && !isFallFromWall)
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (attackInput && player.AttackState.CheckIfCanAttack())
        {
            stateMachine.ChangeState(player.AttackState);
        }
        else if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (isWalled && xInput == core.Movement.FacingDirection)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else
        {
            core.Movement.CheckIfShouldFlip(xInput);
            core.Movement.SetVelocityX(playerData.movementVelocity * xInput);
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
        isFallFromWall = player.WallSlideState.fallFromWall;
    }
}

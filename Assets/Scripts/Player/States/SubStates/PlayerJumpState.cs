using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private bool isGrounded;
    private bool isWalled;

    private bool attackInput;

    private int jumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        jumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        // player.InputHandler.UseJumpInput();
        player.Anim.SetBool("isGrounded", isGrounded);

        player.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;

        DecreaseJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        attackInput = player.InputHandler.AttackInput;

        if (player.InputHandler.JumpInputStop)
        {
            player.SetVelocityY(player.CurrentVelocity.y * playerData.jumpHeightMultiplier);
        }

        if (attackInput && player.AttackState.CheckIfCanAttack())
        {
            stateMachine.ChangeState(player.AttackState);
        }

        player.CheckIfShouldFlip(xInput);
        player.SetVelocityX(playerData.movementVelocity * xInput);
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

    public bool CanJump()
    {
        return jumpsLeft > 0 ? true : false;
    }

    public void ResetJumpsLeft() => jumpsLeft = playerData.amountOfJumps;

    public void DecreaseJumpsLeft() => jumpsLeft--;

    public void DecreaseWallJumpsLeft() => jumpsLeft -= playerData.amountOfJumps;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private bool isWalled;

    private bool attackInput;
    private bool shieldInput;

    private int jumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        jumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.PlaySoundEffect("Jump");

        player.Anim.SetBool("isGrounded", isGrounded);

        core.Movement.SetVelocityY(playerData.jumpVelocity);
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
        shieldInput = player.InputHandler.ShieldInput;

        if (player.InputHandler.JumpInputStop)
        {
            core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerData.jumpHeightMultiplier);
        }

        if (!isExitingState)
        {
            if (attackInput && player.AttackState.CheckIfCanAttack())
            {
                stateMachine.ChangeState(player.AttackState);
            }
            else if (shieldInput && PlayerStats.Instance.ShieldDurability.CurrentValue > 0)
            {
                stateMachine.ChangeState(player.ShieldState);
            }

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
        
        isWalled = core.CollisionSenses.CheckIfWalled();
    }

    public bool CheckIfCanJump()
    {
        return jumpsLeft > 0 ? true : false;
    }

    public void ResetJumpsLeft() => jumpsLeft = playerData.amountOfJumps;

    public void DecreaseJumpsLeft() => jumpsLeft--;

    public void DecreaseWallJumpsLeft() => jumpsLeft -= playerData.amountOfJumps;
}

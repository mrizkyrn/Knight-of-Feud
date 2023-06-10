using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    public bool CanAttack { get; private set; }
    public int attackCount { get; private set; }

    private bool attackInput;
    private float lastAttackTime;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // player.PlaySoundEffect("Attack1", 0.3f);

        player.InputHandler.UseAttackInput();
        lastAttackTime = Time.time;
        // attackCount = 0;
        
        player.Anim.SetTrigger("attack1");
    }

    public override void Exit()
    {
        base.Exit();

        player.Anim.ResetTrigger("attack1");
        player.Anim.ResetTrigger("attack2");
        player.Anim.ResetTrigger("attack3");
        player.InputHandler.OnAttackEnable();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        attackInput = player.InputHandler.AttackInput;

        if (!isExitingState)
        {
            if (isGrounded && core.Movement.CurrentVelocity.x != 0)
            {
                core.Movement.SetVelocityZero();
            }

            if (attackInput)
            {
                player.InputHandler.UseAttackInput();

                if (attackCount < 3)
                {
                    // if (attackCount == 1)
                    //     player.PlaySoundEffect("Attack2", 0.3f);
                    // else
                    //     player.PlaySoundEffect("Attack3", 0.3f);
                    attackCount++;
                    lastAttackTime = Time.time;
                }   
                else
                {
                    attackCount = 2;
                    player.InputHandler.OnAttackDisable();
                }
            }

            if (Time.time >= lastAttackTime + playerData.comboDelay)
            {
                attackCount = 0;
                isAbilityDone = true;
                CanAttack = false;
            }
        }
    }

    public bool CheckIfCanAttack()
    {
        return Time.time >= lastAttackTime + playerData.attackCooldown;
    }
}

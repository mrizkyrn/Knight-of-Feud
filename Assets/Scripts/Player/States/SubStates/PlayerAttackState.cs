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

        player.InputHandler.UseAttackInput();
        lastAttackTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        attackInput = player.InputHandler.AttackInput;

        if (!isExitingState)
        {
            if (attackInput)
            {
                if (attackCount < 3)
                {
                    player.InputHandler.UseAttackInput();

                    attackCount++;
                    attackCount = Mathf.Clamp(attackCount, 0, 2);

                    lastAttackTime = Time.time;
                }
                else
                {
                    player.InputHandler.OnAttackDisable();
                }
            }


            if (Time.time >= lastAttackTime + playerData.comboDelay)
            {
                attackCount = 0;
                player.Anim.SetBool("attack2", false);
                player.Anim.SetBool("attack3", false);

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

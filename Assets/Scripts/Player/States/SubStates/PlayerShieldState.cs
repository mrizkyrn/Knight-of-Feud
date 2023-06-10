using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldState : PlayerAbilityState
{
    private bool shieldInput;

    public PlayerShieldState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.IsShielding = true;
    }

    public override void Exit()
    {
        base.Exit();
        player.IsShielding = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        shieldInput = player.InputHandler.ShieldInput;


        if (isGrounded && core.Movement.CurrentVelocity.x != 0)
        {   
            core.Movement.SetVelocityZero();
        }

        if (!shieldInput)
        {
            isAbilityDone = true;
        }
        else if (player.playerStats.ShieldDurability.CurrentValue <= 0)
        {
            player.PlaySoundEffect("BrokenShield");
            isAbilityDone = true;
        }

    }
    
}

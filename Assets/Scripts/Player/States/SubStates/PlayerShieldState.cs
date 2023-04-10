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
    }

    public override void Exit()
    {
        base.Exit();
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
    }
    
}

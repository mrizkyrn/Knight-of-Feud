using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : PlayerAbilityState
{
    public bool CanSlide { get; private set; }

    private bool isWalled;

    private float lastSlideTime;
    private float slideStartTime;
    private Vector2 slideStartPosition;

    public PlayerSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.PlaySoundEffect("Slide");
        player.InputHandler.UseSlideInput();
        CanSlide = false;

        slideStartTime = Time.time;
        slideStartPosition = player.transform.position;

        player.InputHandler.OnAttackDisable();
    }

    public override void Exit()
    {
        base.Exit();

        lastSlideTime = Time.time;

        player.InputHandler.OnAttackEnable();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= slideStartTime + playerData.maxSlideTime)
        {
            Debug.Log("habis");
            isAbilityDone = true;
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        float slideTime = Time.time - slideStartTime;
        float slideDistance = playerData.slideVelocity * slideTime;

        Vector2 slideDirection = new Vector2(core.Movement.FacingDirection, 0f);
        Vector2 slidePosition = slideStartPosition + slideDirection * Mathf.Min(slideDistance, playerData.maxSlideDistance);
        core.Movement.Rb.MovePosition(slidePosition);

        if (player.InputHandler.SlideInputStop || slideDistance >= playerData.maxSlideDistance || isWalled || !isGrounded)
        {
            isAbilityDone = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isWalled = core.CollisionSenses.CheckIfWalled();
    }

    public bool CheckIfCanSlide()
    {
        return CanSlide && Time.time >= lastSlideTime + playerData.slideCooldown;
    }

    public void ResetCanSlide() => CanSlide = true;
}

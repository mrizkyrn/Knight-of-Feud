using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : PlayerAbilityState
{
    public bool CanSlide { get; private set; }

    private bool isWalled;
    private bool isGrounded;

    private float lastSlideTime;
    private float slideStartTime;
    private Vector2 slideStartPosition;

    public PlayerSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        float slideTime = Time.time - slideStartTime;
        float slideDistance = playerData.slideVelocity * slideTime;

        Vector2 slideDirection = new Vector2(player.FacingDirection, 0f);
        Vector2 slidePosition = slideStartPosition + slideDirection * Mathf.Min(slideDistance, playerData.maxSlideDistance);
        player.Rb.MovePosition(slidePosition);

        if (!isExitingState && player.InputHandler.SlideInputStop || slideDistance >= playerData.maxSlideDistance || isWalled || !isGrounded)
        {
            isAbilityDone = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isWalled = player.CheckIfWalled();
        isGrounded = player.CheckIfGrounded();
    }

    public bool CheckIfCanSlide()
    {
        return CanSlide && Time.time >= lastSlideTime + playerData.slideCooldown;
    }

    public void ResetCanSlide() => CanSlide = true;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerSlideState SlideState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerShieldState ShieldState { get; private set; }

    [SerializeField] private PlayerData playerData;
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    [SerializeField] TMP_Text fpsText;
    #endregion

    #region Other Variables
    private int lastFrameIndex;
    private float[] frameDeltatimeArray;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "jump");
        FallState = new PlayerFallState(this, StateMachine, playerData, "fall");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "jump");
        SlideState = new PlayerSlideState(this, StateMachine, playerData, "slide");
        AttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        ShieldState = new PlayerShieldState(this, StateMachine, playerData, "shield");

        frameDeltatimeArray = new float[50];
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();

        // SHOW FPS
        frameDeltatimeArray[lastFrameIndex] = Time.unscaledDeltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltatimeArray.Length;

        fpsText.text = "fps: " + Mathf.RoundToInt(CalculateFPS()).ToString();
        // Debug.Log(CheckIfGrounded());
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void ComboAttack1Transition()
    {
        if (AttackState.attackCount >= 1)
        {
            Anim.SetTrigger("attack2");
        }
    }

    private void ComboAttack2Transition()
    {
        if (AttackState.attackCount >= 2)
        {
            Anim.SetTrigger("attack3");
        }
    }

    private void MovementStartTrigger()
    {
        Core.Movement.SetVelocityX(playerData.movementAttack[AttackState.attackCount] * Core.Movement.FacingDirection);
    }

    private void MovementStopTrigger()
    {
        Core.Movement.SetVelocityZero();
    }

    private float CalculateFPS()
    {
        float total = 0f;
        foreach (float deltaTime in frameDeltatimeArray)
        {
            total += deltaTime;
        }

        return frameDeltatimeArray.Length / total;
    }

}

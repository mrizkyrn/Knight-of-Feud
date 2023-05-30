using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;

    public Vector2 MovementInput { get; private set; }

    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }

    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool SlideInput {get; private set; }
    public bool SlideInputStop { get; private set; }
    public bool AttackInput {get; private set; }
    public bool ShieldInput {get; private set; }
    public bool ShieldInputStop { get; private set; }

    [SerializeField] private float inputHoldTime = 0.2f;
    
    private float jumpInputStartTime;
    private float slideInputStartTime;
    private float attackInputStartTime;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void Update()
    {
        CheckJumpHoldTime();
        CheckSlideHoldTime();
        CheckAttackHoldTime();
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    public void OnEnable()
    {
        playerInput.PlayerControl.Enable();
        playerInput.PlayerControl.Jump.started += OnJump;
        playerInput.PlayerControl.Jump.canceled += OnJump;
        playerInput.PlayerControl.Slide.started += OnSlide;
        playerInput.PlayerControl.Slide.canceled += OnSlide;
        playerInput.PlayerControl.Attack.started += OnAttack;
        playerInput.PlayerControl.Shield.started += OnShield;
        playerInput.PlayerControl.Shield.canceled += OnShield;
    }

    public void OnAttackEnable()
    {
        playerInput.PlayerControl.Attack.started += OnAttack;
    }

    public void OnDisable()
    {
        playerInput.PlayerControl.Disable();
        playerInput.PlayerControl.Jump.started -= OnJump;
        playerInput.PlayerControl.Jump.canceled -= OnJump;
        playerInput.PlayerControl.Slide.started -= OnSlide;
        playerInput.PlayerControl.Slide.canceled -= OnSlide;
        playerInput.PlayerControl.Attack.started -= OnAttack;
        playerInput.PlayerControl.Shield.started -= OnShield;
        playerInput.PlayerControl.Shield.canceled -= OnShield;
    }

    public void OnAttackDisable()
    {
        playerInput.PlayerControl.Attack.started -= OnAttack;
    }

    public void OnMove()
    {
        MovementInput = playerInput.PlayerControl.Movement.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(MovementInput.x);
        NormInputY = Mathf.RoundToInt(MovementInput.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        else if (context.canceled)
        {   
            JumpInputStop = true;
        }
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SlideInput = true;
            SlideInputStop = false;
            slideInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            SlideInputStop = true;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInput = true;
            attackInputStartTime = Time.time;
        }
    }

    public void OnShield(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ShieldInput = true;
        }
        else if (context.canceled)
        {   
            ShieldInput = false;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    public void UseSlideInput() => SlideInput = false;

    public void UseAttackInput() => AttackInput = false;

    private void CheckJumpHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
    
    private void CheckSlideHoldTime()
    {
        if (Time.time >= slideInputStartTime + inputHoldTime)
        {
            SlideInput = false;
        }
    }

    private void CheckAttackHoldTime()
    {
        if (Time.time >= attackInputStartTime + inputHoldTime)
        {
            AttackInput = false;
        }
    }
}

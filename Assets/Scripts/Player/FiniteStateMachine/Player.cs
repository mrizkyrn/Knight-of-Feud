using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    // public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerSlideState SlideState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }

    [SerializeField] private PlayerData playerData;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public BoxCollider2D Col { get; private set; }
    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 workspace;

    private int lastFrameIndex;
    private float[] frameDeltatimeArray;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "jump");
        FallState = new PlayerFallState(this, StateMachine, playerData, "fall");
        // InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "jump");
        SlideState = new PlayerSlideState(this, StateMachine, playerData, "slide");
        AttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");

        frameDeltatimeArray = new float[50];
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rb = GetComponent<Rigidbody2D>();
        Col = GetComponent<BoxCollider2D>();

        StateMachine.Initialize(IdleState);

        FacingDirection = 1;
    }

    private void Update()
    {
        CurrentVelocity = Rb.velocity;
        StateMachine.CurrentState.LogicUpdate();

        frameDeltatimeArray[lastFrameIndex] = Time.unscaledDeltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltatimeArray.Length;

        // Debug.Log(Mathf.RoundToInt(CalculateFPS()));
        // Debug.Log(CheckIfGrounded());

    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void CheckIfShouldFlip(float xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        Vector2 size = new Vector2(Col.bounds.size.x * 0.9f, Col.bounds.size.y);
        RaycastHit2D rh = Physics2D.BoxCast(Col.bounds.center, size, 0f, Vector2.down, .1f, playerData.platformLayerMask);
        return rh.collider != null;
    }

    public bool CheckIfWalled()
    {
        Vector2 direction = new Vector2(FacingDirection, 0);
        RaycastHit2D rh = Physics2D.Raycast(Col.bounds.center, direction, Col.bounds.extents.x + .1f, playerData.platformLayerMask);
        return rh.collider != null;
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void ComboAttack1Transition()
    {
        if (AttackState.attackCount >= 1)
        {
            Anim.SetBool("attack2", true);
        }
    }

    private void ComboAttack2Transition()
    {
        if (AttackState.attackCount >= 2)
        {
            Anim.SetBool("attack3", true);
        }
    }

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
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

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.yellow;

    //     // Calculate the center and size of the box based on the collider's bounds
    //     Vector2 center = Col.bounds.center;

    //     Vector2 size = new Vector2(Col.bounds.size.x * 0.9f, Col.bounds.size.y);

    //     // Perform a BoxCast2D downwards from the center of the box
    //     RaycastHit2D hit = Physics2D.BoxCast(center, size, 0f, Vector2.down, 0.1f, playerData.platformLayerMask);

    //     // Draw the BoxCast2D gizmo
    //     if (hit.collider != null)
    //     {
    //         Gizmos.color = Color.green;
    //         Gizmos.DrawWireCube(hit.point, size);
    //     }
    //     else
    //     {
    //         Gizmos.DrawWireCube(center, size);
    //     }

    //     Vector2 direction = new Vector2(FacingDirection, 0); // replace with desired direction
    //     float distance = Col.bounds.extents.x + 0.1f; // replace with desired distance

    //     // Calculate start and end points of the raycast
    //     Vector2 origin = Col.bounds.center;
    //     Vector2 end = origin + direction * distance;

    //     // Perform the raycast
    //     RaycastHit2D rh = Physics2D.Raycast(origin, direction, distance, playerData.platformLayerMask);

    //     // Draw the raycast using Gizmos
    //     Gizmos.color = rh.collider != null ? Color.green : Color.red;
    //     Gizmos.DrawLine(origin, end);
    // }

}

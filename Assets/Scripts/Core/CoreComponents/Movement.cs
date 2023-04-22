using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D Rb { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }

    public int FacingDirection { get; private set; }

    private Vector2 workspace;

    protected override void Awake()
    {
        base.Awake();

        Rb = GetComponentInParent<Rigidbody2D>();

        FacingDirection = 1;
    }

    public void LogicUpdate()
    {
        CurrentVelocity = Rb.velocity;
    }

    public void SetVelocityZero()
    {
        Rb.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    
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

    public void SetGravity(float newGravity)
    {
        Rb.gravityScale = newGravity;
    }

    public void CheckIfShouldFlip(float xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public void Flip()
    {
        FacingDirection *= -1;
        Rb.transform.Rotate(0f, 180f, 0f);
    }

}

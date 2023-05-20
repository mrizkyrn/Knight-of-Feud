using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D Rb { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }

    public int FacingDirection { get; private set; }

    public bool CanSetVelocity { get; set; }

    private Vector2 workspace;

    protected override void Awake()
    {
        base.Awake();

        Rb = GetComponentInParent<Rigidbody2D>();

        FacingDirection = 1;

        CanSetVelocity = true;
    }

    public void LogicUpdate()
    {
        CurrentVelocity = Rb.velocity;
    }

    public void SetVelocityZero()
    {
        workspace = Vector2.zero;
        SetFinalVelocity();
    }
    
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetFinalVelocity();
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        SetFinalVelocity();
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        SetFinalVelocity();
    }

    private void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            Rb.velocity = workspace;
            CurrentVelocity = workspace;
        }
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

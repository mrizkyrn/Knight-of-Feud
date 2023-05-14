using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float slopeCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float ledgeCheckDistance;

    [SerializeField] private Transform ledgeCheck;

    public BoxCollider2D Col { get; private set; }
    public RaycastHit2D hitSlope;

    private Vector2 direction;
    private Vector2 size;

    private float slopeAngle;

    private bool isOnSlope;


    protected override void Awake()
    {
        base.Awake();

        Col = GetComponentInParent<BoxCollider2D>();
    }

    public bool CheckIfGrounded()
    {
        size = new Vector2(Col.bounds.size.x * 0.9f, Col.bounds.size.y);
        return Physics2D.BoxCast(Col.bounds.center, size, 0f, Vector2.down, groundCheckDistance, platformLayerMask);   
    }

    public bool CheckIfSloped()
    {
        hitSlope = Physics2D.Raycast(Col.bounds.center, Vector2.down, slopeCheckDistance, platformLayerMask);
        return hitSlope;
    }

    public bool CheckIfWalled()
    {
        direction = new Vector2(core.Movement.FacingDirection, 0);
        return Physics2D.Raycast(Col.bounds.center, direction, Col.bounds.extents.x + wallCheckDistance, platformLayerMask);
    }

    public bool CheckIfLedged()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, ledgeCheckDistance, platformLayerMask);
    }

    private void OnDrawGizmos()
    {
        if (core != null)
        {
            // // Check Ground
            // Gizmos.color = CheckIfGrounded() ? Color.green : Color.red;
            // Gizmos.DrawWireCube(Col.bounds.center, size);

            // // Check Wall
            // Gizmos.color = CheckIfWalled() ? Color.green : Color.red;
            // Gizmos.DrawLine(Col.bounds.center, (Vector2)Col.bounds.center + direction * (Col.bounds.extents.x + wallCheckDistance));

            // Check Ledge
            Gizmos.color = CheckIfLedged() ? Color.green : Color.red;
            Gizmos.DrawLine(ledgeCheck.position, (Vector2)ledgeCheck.position + Vector2.down * ledgeCheckDistance);
        }

    }
}

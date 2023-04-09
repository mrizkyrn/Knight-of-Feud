using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    [SerializeField] LayerMask platformLayerMask;

    public BoxCollider2D Col { get; private set; }

    private Vector2 direction;
    private Vector2 size;

    protected override void Awake()
    {
        base.Awake();

        Col = GetComponentInParent<BoxCollider2D>();
    }

    public bool CheckIfGrounded()
    {
        size = new Vector2(Col.bounds.size.x * 0.9f, Col.bounds.size.y);
        return Physics2D.BoxCast(Col.bounds.center, size, 0f, Vector2.down, .1f, platformLayerMask);   
    }

    public bool CheckIfWalled()
    {
        direction = new Vector2(core.Movement.FacingDirection, 0);
        return Physics2D.Raycast(Col.bounds.center, direction, Col.bounds.extents.x + .1f, platformLayerMask);
    }
}

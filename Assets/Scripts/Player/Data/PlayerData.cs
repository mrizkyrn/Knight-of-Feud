using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public float jumpHeightMultiplier = 0.5f;
    public int amountOfJumps = 2;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2); 

    [Header("WallSlide State")]
    public float grabWallVelocity = 3f;
    public float wallSlideVelocity = 7f;
    public float maxHoldTime = 0.5f;

    [Header("Slide State")]
    public float slideCooldown = 2f;
    public float slideVelocity = 30f;
    public float maxSlideDistance = 5f;
    public float slideDistanceMultiplier = 0.5f;

    [Header("Attack State")]
    public float attackCooldown = 1f;
    public float comboDelay = 0.3f;

    [Header("Others")]
    public float gravityScale = 5f;
}

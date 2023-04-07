// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerInAirState : PlayerState
// {
//     private bool isGrounded;
//     private bool jumpInput;

//     private int xInput;

//     public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
//     {
//     }

//     public override void Enter()
//     {
//         base.Enter();
//     }

//     public override void Exit()
//     {
//         base.Exit();
//     }

//     public override void LogicUpdate()
//     {
//         base.LogicUpdate();

//         xInput = player.InputHandler.NormInputX;
//         jumpInput = player.InputHandler.JumpInput;

//         if (jumpInput)
//         {
//             player.InputHandler.UseJumpInput();
//             stateMachine.ChangeState(player.JumpState);
//         }
//         else if (isGrounded && player.CurrentVelocity.y < 0.01f)
//         {
//             stateMachine.ChangeState(player.LandState);
//         }
//         else
//         {
//             player.CheckIfShouldFlip(xInput);
//             player.SetVelocityX(playerData.movementVelocity * xInput);

//             player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
//         }
//     }

//     public override void PhysicsUpdate()
//     {
//         base.PhysicsUpdate();
//     }

//     public override void DoChecks()
//     {
//         base.DoChecks();

//         isGrounded = player.CheckIfGrounded();
//     }
// }

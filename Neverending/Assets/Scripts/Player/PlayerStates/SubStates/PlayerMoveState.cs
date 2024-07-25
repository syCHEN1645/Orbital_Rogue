using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState 
{
	public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
	}

	public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
	}

	public override void Exit() {
		base.Exit();
	}

	public override void FixedUpdate() {
		base.FixedUpdate();

        if (movementInput != Vector2.zero) {

            bool success = player.TryMove(movementInput);

			if (!success && movementInput.x > 0) {
                success = player.TryMove(new Vector2(movementInput.x, 0));
            }

			if (!success && movementInput.y > 0) {
                success = player.TryMove(new Vector2(0, movementInput.y));
            }
            
            player.Anim.SetBool("Move", success);
        } else {
            stateMachine.ChangeState(player.IdleState);
        }
            
        player.Flip(player.PlayerSpriteRenderer, xInput);
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState {
	public bool CanDash { get; private set; }
	private bool isHolding;
	private bool dashInputStop;
	private float lastDashTime;
	private Vector2 dashDirection;
	private Vector2 dashDirectionInput;

	public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) 
		: base(player, stateMachine, playerData, animBoolName) {
			CanDash = true;
	}

	public override void Enter() {
		base.Enter();
		Debug.Log("velocity" + player.CurrentVelocity);
		CanDash = false;
		player.DashState.ResetCanDash();
		player.InputHandler.UseDashInput();

		dashDirection = Vector2.right * player.FacingDirection;
	}

	public override void Exit() {
		base.Exit();
		Debug.Log("velocity" + player.CurrentVelocity);
		player.SetVelocity(playerData.DashVelocity, player.CurrentVelocity.normalized);
	}

	public override void FixedUpdate() {
		base.FixedUpdate();
		dashDirectionInput = player.InputHandler.DashDirectionInput;
		dashInputStop = player.InputHandler.DashInputStop;

		if (dashDirectionInput != Vector2.zero) {
			dashDirection = dashDirectionInput;
			dashDirection.Normalize();
			Debug.Log(dashDirection);
		} else {
			dashDirection = player.CurrentVelocity.normalized;
		}

		player.TryMove(dashDirection);
    	player.Flip(player.PlayerSpriteRenderer, Mathf.RoundToInt(dashDirection.x));
	}

	public bool CheckIfCanDash() {
		return CanDash && Time.time >= lastDashTime + playerData.DashCooldown;
	}

	public void ResetCanDash() => CanDash = true;
}
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
	private Vector2 lastAIPos;

	public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) 
		: base(player, stateMachine, playerData, animBoolName) {
			CanDash = true;
	}

	public override void Enter() {
		base.Enter();

		CanDash = false;
		player.DashState.ResetCanDash();
		player.InputHandler.UseDashInput();

		dashDirection = Vector2.right * player.FacingDirection;

		//startTime = Time.unscaledTime;
	}

	public override void Exit() {
		base.Exit();
		//Debug.Log(player.CurrentVelocity);
		player.SetVelocity(0, player.CurrentVelocity.normalized);
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		dashDirectionInput = player.InputHandler.DashDirectionInput;
		dashInputStop = player.InputHandler.DashInputStop;

		if (dashDirectionInput != Vector2.zero) {
			dashDirection = dashDirectionInput;
			dashDirection.Normalize();
			Debug.Log(dashDirection);
		} else {
			dashDirection = player.CurrentVelocity.normalized;
		}
    
		player.SetVelocity(playerData.DashVelocity, dashDirection);
    	player.Flip(player.PlayerSpriteRenderer, Mathf.RoundToInt(dashDirection.x));
		//CheckIfShouldPlaceAfterImage();

		/*if (Time.time >= startTime + playerData.DashTime) {
			player.RB.drag = 0f;
			isAbilityDone = true;
			lastDashTime = Time.time;		
		}*/
	}

	public void DashComplete()
	{
		isAbilityDone = true;
	}

	public bool CheckIfCanDash() {
		return CanDash && Time.time >= lastDashTime + playerData.DashCooldown;
	}

	public void ResetCanDash() => CanDash = true;
}
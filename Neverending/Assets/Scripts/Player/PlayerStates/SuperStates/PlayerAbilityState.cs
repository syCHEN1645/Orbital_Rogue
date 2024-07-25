using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState {
	protected bool isAbilityDone;

	private bool isGrounded;

	public PlayerAbilityState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
	}

	public override void DoChecks() {
		base.DoChecks();

	}

	public override void Enter() {
		base.Enter();

		isAbilityDone = false;
	}

	public override void Exit() {
		base.Exit();
	}

	public override void FixedUpdate() {
		base.FixedUpdate();

		if (isAbilityDone) {
			stateMachine.ChangeState(player.IdleState);
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
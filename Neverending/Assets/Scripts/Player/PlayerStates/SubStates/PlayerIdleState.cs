﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState {
	public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
	}

	public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
		player.SetVelocity(0);
	}

	public override void Exit() {
		base.Exit();
	}

	public override void FixedUpdate() {
		base.FixedUpdate();

		if (movementInput != Vector2.zero) {
			stateMachine.ChangeState(player.MoveState);
		} 

	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}

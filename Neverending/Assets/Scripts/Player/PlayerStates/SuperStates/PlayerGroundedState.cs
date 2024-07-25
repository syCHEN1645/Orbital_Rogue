using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Vector2 movementInput;
    protected int xInput;
    protected int yInput;
    private bool dashInput;

    public PlayerGroundedState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        //CheckFacingDirection();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        movementInput = player.InputHandler.MovementInput;
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        dashInput = player.InputHandler.DashInput;
        
        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        } else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary])
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        } else if (dashInput && player.DashState.CheckIfCanDash())
        {
            Debug.Log("check: " + dashInput);
            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckFacingDirection()
    {   
        player.Flip(player.PlayerSpriteRenderer, player.FacingDirection);
    }
}

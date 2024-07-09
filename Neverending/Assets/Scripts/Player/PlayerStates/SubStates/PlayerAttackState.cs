using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;
    private int xInput;
    private float velocityToSet;
    private bool setVelocity;
    private bool CheckFlip;

    public PlayerAttackState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animBoolName,
        Weapon weapon) : base(player, stateMachine, playerData, animBoolName) 
        {
            this.weapon = weapon;

            weapon.OnExit += ExitHandler;
        }

    public override void Enter()
    {
        base.Enter();

        setVelocity = false;

        weapon.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        weapon.Exit();
    }

    private void ExitHandler()
    {
        //AnimationFinishTrigger();
        isAbilityDone = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;

        if (CheckFlip) 
        {
            player.Flip(xInput);
        }

        if (setVelocity)
        {
            player.SetVelocityX(velocityToSet * player.FacingDirection);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.initializeWeapon(this);
    }

    public void SetPlayerVelocity(float velocity)
    {
        player.SetVelocityX(velocity * player.FacingDirection);

        velocityToSet = velocity;
        setVelocity = true;
    }
    
    public void SetFlipCheck(bool value)
    {
        CheckFlip = value;
    } 
}

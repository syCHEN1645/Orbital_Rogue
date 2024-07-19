using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;
    private int xInput;
    private float velocityToSet;
    private bool setVelocity;
    private bool checkFlip;
    private SpriteRenderer baseSpriteRenderer;
    private Camera mainCam;

    public PlayerAttackState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animBoolName,
        Weapon weapon) : base(player, stateMachine, playerData, animBoolName) 
        {
            this.weapon = weapon;
            mainCam = Camera.main;
            weapon.OnExit += ExitHandler;
        }

    public override void Enter()
    {
        base.Enter();

        checkFlip = true;
        setVelocity = false;
        weapon.Enter();
        LockAttackDirection();
    }

    public override void Exit()
    {
        base.Exit();
        weapon.Exit();
        player.SetFacingDirection();
    }

    private void ExitHandler()
    {   
        isAbilityDone = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
            
        if (setVelocity)
        {
            player.SetVelocityX(velocityToSet * player.FacingDirection);
        }
    }
    private void LockAttackDirection()
    {
        if (checkFlip) {
            Vector3 mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
            float playerPositionX = player.transform.position.x;

            // Determine if the mouse is to the left or right of the player
            if (mousePosition.x > playerPositionX) 
            {
                player.FacingDirection = 1;
                weapon.WeaponFlip(false);
            } 
            else if (mousePosition.x < playerPositionX) 
            {
                player.FacingDirection = -1;
                weapon.WeaponFlip(true);
            }
            
            checkFlip = false; // Lock the attack direction
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
    
    private void HandleFlipSetActive(bool value)
    {
        checkFlip = value;
    }
}

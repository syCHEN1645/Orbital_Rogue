using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }
    public PlayerStunState StunState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }

    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    
    public Vector2 CurrentVelocity { get; private set; }
    public SpriteRenderer PlayerSpriteRenderer { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private ContactFilter2D movementFilter;
    private float collisionOffset = 0.05f;
    private Vector2 workspace;
    private Weapon primaryWeapon;
    private Weapon secondaryWeapon;
    public int FacingDirection;
    public PlayerData playerData { get; private set; }
    

    private void Awake()
    {
        playerData = GetComponent<PlayerData>();
        primaryWeapon = GameObject.Find("PrimaryWeapon").GetComponent<Weapon>();
        secondaryWeapon = GameObject.Find("SecondaryWeapon").GetComponent<Weapon>();
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "Move");
        DashState = new PlayerDashState(this, StateMachine, playerData, "Dash");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "Attack", primaryWeapon);
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "Attack", secondaryWeapon);
        StunState = new PlayerStunState(this, StateMachine, playerData, "Stun");
        DeathState = new PlayerDeathState(this, StateMachine, playerData, "Death");
    }

    private void Start() 
    {
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Inventory = GetComponent<PlayerInventory>();

        FacingDirection = 1;

        PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.secondary]);

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.FixedUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityX(float xInput) 
    {
        workspace.Set(xInput, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocity(float Input) 
    {
        Vector2 direction = CurrentVelocity.normalized;
        workspace.Set(Input * direction.x, Input * direction.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void DashComplete()
    {
        SetVelocity(0);
        StateMachine.ChangeState(IdleState);
    }

    public void SetFacingDirection()
    {
        Flip(PlayerSpriteRenderer, FacingDirection);
    }

    public bool TryMove(Vector2 direction) {
        int count = RB.Cast(
            direction,
            movementFilter,
            castCollisions,
            playerData.MovementVelocity * Time.fixedDeltaTime + collisionOffset);

        if (count == 0) {
            RB.MovePosition(RB.position + direction * playerData.MovementVelocity * Time.fixedDeltaTime);
            return true;
        } else {
            return false;
        }
    }

    public void Flip(SpriteRenderer spriteRenderer, int xInput)
    {
        //Debug.Log("face" + FacingDirection);
        if (xInput != 0)
        {
            // Update the facing direction
            FacingDirection = xInput; 
            spriteRenderer.flipX = (FacingDirection == -1);
        }
    }
}
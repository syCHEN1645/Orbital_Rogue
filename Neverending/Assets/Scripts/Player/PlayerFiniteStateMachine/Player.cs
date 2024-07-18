using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }

    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    private Vector2 workspace;
    private Weapon primaryWeapon;
    private Weapon secondaryWeapon;
    public PlayerData playerData { get; private set; }
    

    private void Awake()
    {
        playerData = GetComponent<PlayerData>();
        primaryWeapon = GameObject.Find("PrimaryWeapon").GetComponent<Weapon>();
        secondaryWeapon = GameObject.Find("SecondaryWeapon").GetComponent<Weapon>();
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "Move");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "Attack", primaryWeapon);
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "Attack", secondaryWeapon);
    }

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Inventory = GetComponent<PlayerInventory>();

        FacingDirection = 1;

        PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.secondary]);

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = rb.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetVelocityX(float xInput) 
    {
        workspace.Set(xInput, CurrentVelocity.y);
        rb.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public int getFacingDirection()
    {
        return FacingDirection;
    }

    public bool TryMove(Vector2 direction) {
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            playerData.MovementVelocity * Time.fixedDeltaTime + collisionOffset);

        if (count == 0) {
            rb.MovePosition(rb.position + direction * playerData.MovementVelocity * Time.fixedDeltaTime);
            return true;
        } else {
            return false;
        }
    }

    public void Flip(int xInput)
    {
        Debug.Log("face" + FacingDirection);
        if (xInput != 0 && xInput != FacingDirection)
        {
            //FacingDirection *= -1;
            //rb.transform.Rotate(0.0f, 180.0f, 0.0f);
            FacingDirection = xInput; // Update the facing direction
            spriteRenderer.flipX = (FacingDirection == -1);
        }
    }
        
}
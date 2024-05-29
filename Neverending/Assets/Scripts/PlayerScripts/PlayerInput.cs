using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Animator animator;
    public float groundCheckDistance = 0.1f;
    public float wallRaycastDistance = 0.1f;
    public ContactFilter2D groundCheckFilter;
    public PlayerAttack playerAttack;
    private Rigidbody2D rb;
    private Collider2D collider2d;
    private List<RaycastHit2D> groundHits = new List<RaycastHit2D>();
    private List<RaycastHit2D> wallHits = new List<RaycastHit2D>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        collider2d = GetComponent<Collider2D>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw(PAP.axisXinput);

        animator.SetFloat(PAP.moveX, moveX);

        bool isMoving = !Mathf.Approximately(moveX, 0f);

        animator.SetBool(PAP.isMoving, isMoving);

        bool lastOnGround = animator.GetBool(PAP.isOnGround);
        bool newOnGround = CheckIfOnGround();
        animator.SetBool(PAP.isOnGround, newOnGround);

        if (lastOnGround == false && newOnGround == true) {
            animator.SetTrigger(PAP.landedOnGround);
        }

        bool onWall = CheckIfOnWall();
        animator.SetBool(PAP.isOnWall, onWall);

        bool isJumpKeyPressed = Input.GetButtonDown(PAP.jumpKeyName);

        if(isJumpKeyPressed){
            animator.SetTrigger(PAP.JumpTriggerName);
        } else {
            animator.ResetTrigger(PAP.JumpTriggerName);
        }

        // attack
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool(PAP.isAttacking, true);
            //
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            playerAttack.DealDamage(enemy.GetComponent<EnemyHealth>());
        }
    }

    void FixedUpdate() {
        float forceX = animator.GetFloat(PAP.forceX);
        
        if (forceX != 0) rb.AddForce(new Vector2(forceX, 0) * Time.deltaTime);

        float impulseX = animator.GetFloat(PAP.impulseX);
        float impulseY = animator.GetFloat(PAP.impulseY);
        
        if (impulseY != 0 || impulseX != 0) {
            float xDirectionSign = Mathf.Sign(transform.localScale.x);
            Vector2 impulseVector = new Vector2(xDirectionSign * impulseX, impulseY);

            rb.AddForce(impulseVector, ForceMode2D.Impulse);
            animator.SetFloat(PAP.impulseY, 0);
            animator.SetFloat(PAP.impulseX, 0);  
        }

        animator.SetFloat(PAP.velocityY, rb.velocity.y);

        bool isStopVelocity = animator.GetBool(PAP.stopVelocity);

        if (isStopVelocity) {
            rb.velocity = Vector2.zero;
            animator.SetBool(PAP.stopVelocity, false);
        }
    }

    bool CheckIfOnGround() {
        collider2d.Cast(Vector2.down, groundCheckFilter, groundHits, groundCheckDistance);

        if(groundHits.Count > 0) {
            return true;
        } else {
            return false;
        }
    }
    bool CheckIfOnWall() {
        Vector2 localScale = transform.localScale;

        collider2d.Raycast(Mathf.Sign(localScale.x) * Vector2.right, groundCheckFilter, wallHits, wallRaycastDistance);

        if(wallHits.Count > 0) {
            return true;
        } else {
            return false;
        }
    }

    public void endAttack()
    {
        animator.SetBool(PAP.isAttacking, false);
    }
}

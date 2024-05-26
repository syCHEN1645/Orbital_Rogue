using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Animator animator;
    public float groundCheckDistance = 0.1f;
    public ContactFilter2D groundCheckFilter;

    private Rigidbody2D rb;
    private Collider2D collider2d;
    private List<RaycastHit2D> groundHits = new List<RaycastHit2D>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        collider2d = GetComponent<Collider2D>();
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

        bool isJumpKeyPressed = Input.GetButtonDown(PAP.jumpKeyName);

        if(isJumpKeyPressed){
            animator.SetTrigger(PAP.JumpTriggerName);
        } else {
            animator.ResetTrigger(PAP.JumpTriggerName);
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
    }

    bool CheckIfOnGround() {
        collider2d.Cast(Vector2.down, groundCheckFilter, groundHits, groundCheckDistance);

        if(groundHits.Count > 0) {
            return true;
        } else {
            return false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    private PlayerAttack playerAttack;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    bool canMove = true;
    public bool isBlocking = false;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
 
    // Update is called once per frame
    private void FixedUpdate() {
        if (canMove) {
            if (movementInput != Vector2.zero) {

                bool success = TryMove(movementInput);

                if (!success && movementInput.x > 0) {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if (!success && movementInput.y > 0) {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                
                animator.SetBool("isMoving", success);
            } else {
                animator.SetBool("isMoving", false);
            }

            //Set direction of sprite to movement direction
            if (movementInput.x > 0) {
                spriteRenderer.flipX = false;
            } else if (movementInput.x < 0){
                spriteRenderer.flipX = true;
            }
        }
    }

    private bool TryMove(Vector2 direction) {
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0) {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        } else {
            return false;
        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = moveSpeed * movementValue.Get<Vector2>();
    }

    void OnFire() {
        animator.SetTrigger("Attack");
        foreach(Enemy enemy in Enemy.enemyList) {
            if (playerAttack.WithinAttackRange(enemy) && !playerAttack.IsAttacking()) {
                StartCoroutine(playerAttack.DealDamage(enemy.GetComponent<EnemyHealth>()));
            }
        };
    }

    void OnBlock() {
        animator.SetTrigger("Block");
    }

    void OnDash() {
        animator.SetTrigger("Dash");
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }

    public void Injure() {
        animator.SetTrigger("isInjured");
    }

    public void Die() {
        animator.SetTrigger("Death");
    }

    protected void BodyDisappear() {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBandit1 : Enemy
{
    [SerializeField]
    protected float patrolRange, healthBarOffset;
    protected override void InitialiseEnemy() {
        base.InitialiseEnemy();
        speed = 0.7f;
        attack = 15.0f;
        attackRange = 1.0f;
        attackInterval = 1.0f;
        spriteScale = 1.0f;
        patrolRange = 0.3f;
        healthBarOffset = 1.4f;
    }
    void Start()
    {
        InitialiseEnemy();      
        animator.transform.localScale = new Vector3(-spriteScale, spriteScale, spriteScale);
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealth.healthBar.transform.position = transform.position + new Vector3(0, healthBarOffset, 0);
        if (enemyHealth.IsDead()) {
            Die();
        } else {
            if (WithinAttackRange(attackRange) && !isAttacking) {
                StartCoroutine(AttackPlayer());
            } else {
                Patrol();
            }
        }
    }

    public override void Attack() {
        base.Attack();
        animator.SetTrigger("Attack");
    }

    public override void Injure() {
        base.Injure();
        animator.SetTrigger("Hurt");
    }

    public override void Die() {
        base.Die();
        // animate death
        animator.SetTrigger("Death");
    }

    // run
    void MoveRight() {
        // face right
        animator.transform.localScale = new Vector3(-spriteScale, spriteScale, spriteScale);
        dir = 'r';
        // move right
        gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
        animator.SetInteger("AnimState", 2);
    }

    void MoveLeft() {
        // face left
        animator.transform.localScale = new Vector3(spriteScale, spriteScale, spriteScale);
        dir = 'l';
        // move left
        gameObject.transform.Translate(-speed * Time.deltaTime, 0, 0);
        animator.SetInteger("AnimState", 2);
    }

    void Patrol() {
        bool withinLeftPatrolBoundary = transform.position.x - originalPosition.x > -patrolRange;
        bool withinRightPatrolBoundary = transform.position.x - originalPosition.x < patrolRange;
        if (!withinLeftPatrolBoundary) {
            // exceed left boundary
            MoveRight();
        } else if (!withinRightPatrolBoundary) {
            // exceed right boundary
            MoveLeft();
        } else {
            // within boundary, move in current dir
            // if blocked, change dir
            if (dir == 'l') {
                MoveLeft();
            } else if (dir == 'r') {
                MoveRight();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) {
            // if collide with a wall, change movement dir
            // Debug.Log("blocked");
            ChangeDir();
        }
    }

    private void ChangeDir() {
        if (dir == 'l') {
            MoveRight();
        } else if (dir == 'r') {
            MoveLeft();
        }
    }
}

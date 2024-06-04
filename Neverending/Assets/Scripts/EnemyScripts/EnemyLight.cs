using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLight : Enemy
{
    public EnemyGroundSensor groundSensor;
    public EnemyLeftSensor leftSensor;
    private float patrolRange = 10.0f;
    protected override void SetParameters() {
        base.SetParameters();
        // hitpoint = attacker's attack * defender's defence
        // health -= hitpoint
        // health is managed by Health
        speed = 1.0f;
        attack = 15.0f;
        attackRange = 1.0f;
        attackInterval = 1.0f;
    }
    void Start()
    {
        SetParameters();
        animator.transform.localScale = new Vector3(-1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.IsDead()) {
            Die();        
            StartCoroutine(bodyDisappear());
        } else {
            if (WithinAttackRange() && !isAttacking) {
                StartCoroutine(AttackPlayer());
            } else {
                Patrol();
            }
        }
    }

    public override void Attack() {
        animator.SetTrigger("Attack");
    }

    public override void Injure() {
        animator.SetTrigger("Hurt");
    }

    public override void Die() {
        // animate death
        animator.SetTrigger("Death");
    }

    private IEnumerator bodyDisappear() {
        // wait for 2 seconds then body will disappear.
        yield return new WaitForSeconds(2);
        // remove sprite
        Destroy(gameObject);
    }

    // run
    void MoveRight() {
        // face right
        animator.transform.localScale = new Vector3(-1, 1, 1);
        dir = 'r';
        // move right
        gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
        animator.SetInteger("AnimState", 2);
    }

    void MoveLeft() {
        // face left
        animator.transform.localScale = new Vector3(1, 1, 1);
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
            if (leftSensor.IsBlocked()) {
                ChangeDir();
            } else {
                if (dir == 'l') {
                    MoveLeft();
                } else if (dir == 'r') {
                    MoveRight();
                }
            }
        }
    }

    private void ChangeDir() {
        if (dir == 'l') {
            MoveRight();
        } else if (dir == 'r') {
            MoveLeft();
        }
    }

    private IEnumerator AttackPlayer() {
        isAttacking = true;
        Attack();
        player.GetComponent<PlayerHealth>().TakeDamage(attack);
        yield return new WaitForSeconds(attackInterval);
        isAttacking = false;
    }
}

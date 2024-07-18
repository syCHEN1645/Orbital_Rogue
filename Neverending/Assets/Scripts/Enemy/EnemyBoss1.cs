using System.Collections;
using UnityEngine;

public class EnemyBoss1 : Enemy
{
    [SerializeField]
    // enemy stops at a distance of stopMovingDistance away from Player
    // enemy starts hunting if Player is closer than huntRange
    protected float healthBarOffset, stopMovingDistance, huntRange, rangedAttackRange;
    [SerializeField]
    protected float meleeAttack, rangedAttack;

    void Start()
    {
        InitialiseEnemy();
        
        // Attack();
        Cast();
        // Spell();
        // Walk();
        // Injure();
        
    }

    void Update()
    {
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) <= huntRange) {
            HuntPlayer();
        } else {
            // idle around
        }
        // Walk();
    }

    protected override void InitialiseEnemy()
    {
        base.InitialiseEnemy();
        speed = 2.0f;
        // attack is by default referring to melee attack if both melee and ranged attacks exist
        attack = 15.0f;
        attackRange = 1.0f;
        attackInterval = 1.0f;
        spriteScale = 4.0f;

        healthBarOffset = 1.4f;
        stopMovingDistance = 0.5f;
        huntRange = 15.0f;
        rangedAttack = 40.0f;
        rangedAttackRange = 5.0f;

        enemyHealth.SetDefense(30);
        enemyHealth.SetMaxHealth(100);
    }

    public override void Attack()
    {
        animator.SetTrigger("Attack");
        // animator.ResetTrigger("Attack");
    }

    protected void Cast() {
        animator.SetTrigger("Cast");
    }
    protected void Spell() {
        animator.SetTrigger("Spell");
    }

    protected void Walk() {
        animator.SetBool("Walk", true);
    }
    public override void Injure() {
        animator.SetTrigger("Hurt");
    }

    protected bool WithinHuntRange() {
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) <= huntRange) {
            return true;
        }
        return false;
    }

    protected void HuntPlayer()
    {
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) > 0) {
            MoveTowardsPlayer();
        }
        if (WithinAttackRange(attackRange)) {
            Debug.Log("Attack");
            StartCoroutine(AttackPlayer());
        }
    }

    protected void MoveTowardsPlayer()
    {
        // vector pointing to Player
        var unitVector = GetUnitVectorTowardsPlayer();
        gameObject.transform.Translate(
            speed * Time.deltaTime * unitVector.x, 
            speed * Time.deltaTime * unitVector.y, 
            0);
        // left / right
        // if (unitVector.x > 0) {
        //     dir = 'r';
        //     animator.transform.localScale = new Vector3(spriteScale, spriteScale, spriteScale);
        // } else {
        //     dir = 'l';
        //     animator.transform.localScale = new Vector3(-spriteScale, spriteScale, spriteScale);
        // }
        // animation
        Walk();
    }

    protected Vector3 GetUnitVectorTowardsPlayer() {
        // x = x, y = y, z = 0
        return Vector3.Normalize(player.transform.position - gameObject.transform.position);
    }

    protected IEnumerator RangedAttackPlayer() {
        isAttacking = true;
        // ranged attack animation
        Cast();

        if (playerHealth != null) {
            playerHealth.TakeDamage(rangedAttack);
        }
        yield return new WaitForSeconds(attackInterval);
        isAttacking = false;
    }

    // protected void Attack3() {
    //     // melee attack
    //     Attack();
    // }

    // void MoveRight() {
    //     // face right
    //     animator.transform.localScale = new Vector3(-spriteScale, spriteScale, spriteScale);
    //     dir = 'r';
    //     // move right
    //     gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
    //     animator.SetInteger("AnimState", 2);
    // }

    // void MoveLeft() {
    //     // face left
    //     animator.transform.localScale = new Vector3(spriteScale, spriteScale, spriteScale);
    //     dir = 'l';
    //     // move left
    //     gameObject.transform.Translate(-speed * Time.deltaTime, 0, 0);
    //     animator.SetInteger("AnimState", 2);
    // }
    // private void ChangeDir() {
    //     if (dir == 'l') {
    //         MoveRight();
    //     } else if (dir == 'r') {
    //         MoveLeft();
    //     }
    // }
}

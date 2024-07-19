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

    protected Vector3 previousVector;
    
    // an empty object indicating centre of boss
    public GameObject centre;
    protected float centreOffset;

    void Start()
    {
        InitialiseEnemy();
        
        // Attack();
        // Cast();
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
        attackRange = 2.0f;
        attackInterval = 0.75f;
        spriteScale = 4.0f;

        healthBarOffset = 1.4f;
        stopMovingDistance = 0.2f;
        huntRange = 15.0f;
        rangedAttack = 40.0f;
        rangedAttackRange = 5.0f;

        enemyHealth.SetDefense(30);
        enemyHealth.SetMaxHealth(100);

        previousVector = GetUnitVectorTowardsPlayer();
        
    }

    public override void Attack()
    {
        animator.SetTrigger("Attack");
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

    protected void StopWalk() {
        animator.SetBool("Walk", false);
    }

    public override void Injure() {
        animator.SetTrigger("Hurt");
    }

    protected bool WithinHuntRange() {
        if (Vector2.Distance(player.transform.position, centre.transform.position) <= huntRange) {
            return true;
        }
        return false;
    }

    protected void HuntPlayer()
    {
        if (isAttacking) {
            // if attacking, do not move
            StopWalk();
        } else {
            // if not attacking, move towards Player
            if (Vector2.Distance(player.transform.position, centre.transform.position) > stopMovingDistance) {
                MoveTowardsPlayer();
            }
            if (WithinAttackRange(attackRange)) {
                Debug.Log("Attack");
                StopWalk();
                StartCoroutine(AttackPlayer());
            }
        }
        
    }

    protected void MoveTowardsPlayer()
    {
        // vector pointing to Player
        // Debug.Log("pos " + centre.transform.position.x);
        // Debug.Log("local " + centre.transform.localPosition.x);
        var unitVector = GetUnitVectorTowardsPlayer();
        gameObject.transform.Translate(
            speed * Time.deltaTime * unitVector.x, 
            speed * Time.deltaTime * unitVector.y, 
            0);
        
        // flip
        if (unitVector.x * previousVector.x < 0) {
            // dir has changed
            Flip();
            // left / right
            if (unitVector.x > 0) {
                dir = 'r';
            } else {
                dir = 'l';
            }
        }

        // animation
        Walk();
        // current vector becomes previous vector
        previousVector = unitVector;
    }

    private void Flip()
    {
        animator.transform.localScale = new Vector3(
            -animator.transform.localScale.x, 
            animator.transform.localScale.y, 
            animator.transform.localScale.z);
        // this line if put in Start() will return 0, maybe due to
        centreOffset = Mathf.Abs(gameObject.transform.position.x - centre.transform.position.x);
        if (animator.transform.localScale.x < 0) {
            // if now x < 0 i.e. face left, shift left
            gameObject.transform.Translate(-2 * centreOffset, 0 , 0);
            Debug.Log(-2 * centreOffset);
        } else {
            // if now x > 0 i.e. face right, shift right
            gameObject.transform.Translate(2 * centreOffset, 0 , 0);
            Debug.Log(2 * centreOffset);
        }
    }

    protected Vector3 GetUnitVectorTowardsPlayer() {
        // x = x, y = y, z = 0
        return Vector3.Normalize(player.transform.position - centre.transform.position);
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

